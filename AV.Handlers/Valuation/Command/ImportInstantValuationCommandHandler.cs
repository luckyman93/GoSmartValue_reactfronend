using AutoMapper;
using AV.Common.Entities;
using AV.Common.Interfaces.UnitOfWorks;
using AV.Contracts.Enums;
using AV.Contracts.Models;
using AV.Contracts.Models.Valuation;
using AV.Contracts.Models.Valuation.Commands;
using AV.Contracts.Models.Valuation.ResponseModels;
using AV.Handlers.Valuation.Mappers;
using AV.Handlers.Valuation.Validators;
using AV.Persistence.Queries;
using AV.Persistence.Stores.Valuations;
using CsvHelper;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ImportHeader = AV.Common.Entities.ImportHeader;
using Locality = AV.Common.Entities.Locality;
using Location = AV.Common.Entities.Location;
using Street = AV.Common.Entities.Street;

namespace AV.Handlers.Valuation.Command
{
    public class ImportInstantValuationCommandHandler : IRequestHandler<ImportInstantValuationCommand, ImportForInstantValuationResponse>
    {
        private int _numberOfRecordForComparable = 3;

        private readonly InstantValuationValidators _instantValuationValidators;
        private readonly ILogger<ImportInstantValuationCommandHandler> _logger;
        private readonly IMapper _mapper;
        private ILocationUnitOfWork _locationUow;
        private IComparableUnitOfWork _comparableUnit;
        private readonly IComparablesQueries _comparablesQueries;
        private readonly ILocationsQueries _locationsQueries;
        private readonly IComparableStore _comparableStore;

        private IEnumerable<Comparable> allComparables { get; set; }
        private IEnumerable<ComparableBandSize> bands { get; set; }
        private IEnumerable<LandRate> landRates { get; set; }

        public ImportInstantValuationCommandHandler(
            InstantValuationValidators instantValuationValidators,
            ILogger<ImportInstantValuationCommandHandler> logger,
            IMapper mapper,
            ILocationUnitOfWork locationUow,
            IComparableUnitOfWork comparableUnit,
            IComparablesQueries comparablesQueries,
            ILocationsQueries locationsQueries,
            IComparableStore comparableStore)
        {
            _instantValuationValidators = instantValuationValidators;
            _logger = logger;
            _mapper = mapper;
            _locationUow = locationUow;
            _comparableUnit = comparableUnit;
            _comparablesQueries = comparablesQueries;
            _locationsQueries = locationsQueries;
            _comparableStore = comparableStore;
        }

        public async Task<ImportForInstantValuationResponse> Handle(ImportInstantValuationCommand command, CancellationToken cancellationToken)
        {
            var fileStream = ConvertFormFileToStream(command.CsvFormFile);
            var importHeader = new ImportHeader
            {
                FileName = command.CsvFormFile.FileName,
                FileStream = fileStream,
                Type = command.ImportType,
                ImportedById = command.ImportedBy,
                ActiveFrom = DateTimeOffset.UtcNow,
                ActiveTo = DateTimeOffset.UtcNow
            };

            var rawComparableRequests = ReadCsvFile(fileStream, importHeader);

            var result = ValidateRawInstantValuationRecords(rawComparableRequests);
            if (result.Count > 0)
            {
                return new ImportForInstantValuationResponse
                {
                    Header = _mapper.Map<Contracts.Models.ImportHeader>(importHeader),
                    FailedData = _mapper.Map<IEnumerable<ComparableRequestViewModel>>(rawComparableRequests),
                    ImportErrors = result,
                    Status = ImportStatus.FailedValidation
                };
            }
            //Service: SetLocationDetails
            var comparableRequests = _mapper.Map<IEnumerable<ComparableRequestViewModel>>(rawComparableRequests);
            SetLocationDetails(comparableRequests);

            //validate comparable requests
            IEnumerable<ComparableResultViewModel> results = new List<ComparableResultViewModel>();
            if (ValidateComparableRequest(comparableRequests).Count == 0)
            {
                //if no validation perform Bulk Comparable calculations
                results = await PerformBulkComparableCalculation(comparableRequests);
            }
            return await CreateImportForInstantValuationResponse(comparableRequests, results);
        }

        private async Task<IEnumerable<ComparableResultViewModel>> PerformBulkComparableCalculation(
            IEnumerable<ComparableRequestViewModel> comparableRequests)
        {
            var compRequests = _mapper.Map<IEnumerable<Comparable>>(comparableRequests);
            //Performs Comparable
            var comparableResults = await PerformComparables(compRequests);

            var comparablesList = new List<ComparableResultViewModel>();
            foreach (var comp in comparableResults)
            {
                comparablesList.Add(new ComparableResultViewModel
                {
                    EstimatedValue = comp.EstimatedValue,
                    ComparableResultId = comp.ReferenceNumber,
                    ReferenceNumber = comp.ReferenceNumber,
                    ComparableId = comp.ComparableId,
                    ComparableRequest = _mapper.Map<ComparableRequestViewModel>(comp.Comparable),
                    Comparables = _mapper.Map<List<ComparableViewModel>>(comp?.Comparables?.ToList()),
                    ReportRequested = false
                });
            }
            return comparablesList;
        }

        private async Task<IEnumerable<ComparableResult>> PerformComparables(IEnumerable<Comparable> compRequests)
        {
            if (!compRequests.Any())
                return new List<ComparableResult>();

            SetLocationDetails(compRequests);
            await _comparableStore.Create(compRequests, new CancellationToken());

            allComparables = await _comparablesQueries.FetchAllVerified(default);
            bands = await _comparablesQueries.GetAllComparableBandClass();
            landRates = await _comparablesQueries.GetAllLandRates();

            var results = new List<ComparableResult>();
            foreach (var request in compRequests)
            {
                results.Add(PerformComparable(request));
            }

            await _comparableStore.CreateResults(results, new CancellationToken());

            return results;
        }

        public ComparableResult PerformComparable(Comparable comparableRequest)
        {
            comparableRequest.DataState = DataState.Raw;
            comparableRequest.PlotSize = ConvertToMetreSquared(comparableRequest.PlotSize, comparableRequest.Metric);
            comparableRequest.BandClass = CalculateBandClass(comparableRequest.PlotSize);

            var comparableResult = new ComparableResult
            {
                ComparableId = comparableRequest.Id,
                ReferenceNumber = Guid.NewGuid(),
                EstimatedOn = DateTimeOffset.UtcNow,
            };

            if (comparableRequest.PropertyType == PropertyType.Vacant)
            {
                var landRate = GetLandRate(comparableRequest);
                decimal estimate = 0;
                estimate = GetVacantPlotEstimate(comparableRequest, landRate, estimate);

                comparableRequest.SalePrice = estimate;
                comparableResult.EstimatedValue = (double)estimate;
            }
            else
            {
                var comparablesByDescending = GetComparableMatches(comparableRequest)?.ToList() ?? new List<Comparable>();
                if (!comparablesByDescending.Any() || comparablesByDescending.Count() < _numberOfRecordForComparable)
                {
                    GetComparableMatchFillers(comparableRequest, comparablesByDescending,
                        _numberOfRecordForComparable - comparablesByDescending.Count);
                }

                //Average Sale Price
                decimal avgSalePrice = 0;
                if (comparablesByDescending.Any())
                    avgSalePrice = comparablesByDescending.Average(c => c.SalePrice ?? 0);

                if (avgSalePrice > 0)
                {
                    comparableRequest.SalePrice = avgSalePrice;
                    comparableResult.EstimatedValue = (double)avgSalePrice;
                    AttachComparablesToResult(comparableResult, comparablesByDescending);
                }
                else
                {
                    var landRate = GetLandRate(comparableRequest);
                    decimal estimate = 0;
                    estimate = GetVacantPlotEstimate(comparableRequest, landRate, estimate);

                    comparableRequest.SalePrice = estimate;
                    comparableResult.EstimatedValue = (double)estimate;
                }
            }
            return comparableResult;
        }

        private static void AttachComparablesToResult(ComparableResult comparableResult, ICollection<Comparable> comparables)
        {
            var comparableResultLinks
                = new List<ComparableResultComparable>();
            foreach (var c in comparables)
            {
                comparableResultLinks.Add(
                    new ComparableResultComparable
                    {
                        Comparable = c
                    });
            }

            comparableResult.Comparables = comparableResultLinks;
        }

        private IEnumerable<Comparable> GetComparableMatches(Comparable comparableRequest)
        {
            return allComparables
                .Where(v =>
                    //Same Land use
                    v.LandUse == comparableRequest.LandUse &&
                    //Verified Records
                    v.DataState == DataState.Verified &&
                    //Add within the same band
                    v.BandClass?.BandName == comparableRequest.BandClass.BandName &&
                    //And sale price is greater than 0
                    v.SalePrice > 0 &&
                    //Same City/Town
                    v.LocationId == comparableRequest.LocationId &&
                    //same locality or Locality is not set under conditions
                    (
                        // don't match on locality if locality has no value
                        !comparableRequest.LocalityId.HasValue
                        //or Locality record is not verified
                        //|| !comparableRequest.Locality.Verified
                        //or locality id is 0
                        || comparableRequest.LocalityId == 0
                        //otherwise include where locality id matches existing comparable locality ids
                        || v.LocalityId == comparableRequest.LocalityId)
                )
                .OrderByDescending(v => v.DateOfSale)
                .Take(_numberOfRecordForComparable);
        }

        private void GetComparableMatchFillers(Comparable comparableRequest, List<Comparable> matchedComparables, int numberOfFillersNeeded)
        {
            var comparableFillers = allComparables
                .Where(v =>
                    //Verified Records
                    v.DataState == DataState.Verified &&
                    //Add within the same band
                    v.BandClass?.BandName == comparableRequest.BandClass.BandName &&
                    //And sale price is greater than 0
                    v.SalePrice > 0 &&
                    //Same City/Town
                    v.LocationId == comparableRequest.LocationId &&
                    //Record not allready matched
                    !matchedComparables.Any(m => v.Id == m.Id)
                )
                .OrderByDescending(v => v.DateOfSale)
                .Take(numberOfFillersNeeded);

            matchedComparables.AddRange(comparableFillers);
        }

        private decimal GetVacantPlotEstimate(Comparable comparableRequest, LandRate landRate, decimal estimate)
        {

            if (landRate != null)
            {
                estimate = (comparableRequest.BandClass.BandName) switch
                {
                    "LowIncome" => comparableRequest.PlotSize * landRate.LowIncome,
                    "MiddleIncome" => comparableRequest.PlotSize * landRate.MiddleIncome,
                    "HighIncome" => comparableRequest.PlotSize * landRate.HighIncome,
                    _ => estimate
                };
            }

            return estimate;
        }

        private LandRate GetLandRate(Comparable comparableRequest)
        {
            return landRates
                .FirstOrDefault(l => l.LocationId == comparableRequest.LocationId &&
                                     (
                                         !comparableRequest.LocalityId.HasValue
                                         || l.LocalityId == comparableRequest.LocalityId)
                );
        }

        private ComparableBandSize CalculateBandClass(decimal plotSize)
        {
            return bands.OrderByDescending(c => c.LowerBandLimit)
                       .FirstOrDefault(cb =>
                           //size in band
                           (plotSize >= cb.LowerBandLimit && plotSize < cb.UpperBandLimit)
                       ) ??
                   bands.OrderBy(c => c.LowerBandLimit)
                       .First();
        }

        private decimal ConvertToMetreSquared(decimal? plotSize, Metric metric)
        {
            if (!plotSize.HasValue) return 0;
            return plotSize.Value * Constants.GetMetricMultiplierToMeterSquared(metric);
        }
        private byte[] ConvertFormFileToStream(IFormFile csvFormFile)
        {
            if (csvFormFile.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    csvFormFile.CopyTo(ms);
                    return ms.ToArray();
                }
            }
            return null;
        }

        private IEnumerable<ComparableRequestViewModel> ReadCsvFile(byte[] fileByteArray, ImportHeader importHeader)
        {
            // Read the file and display it line by line.  
            try
            {
                using var fileStream = new MemoryStream(fileByteArray);
                using TextReader reader = new StreamReader(fileStream);
                using var csv = new CsvReader(reader, CultureInfo.CurrentCulture);
                csv.Context.RegisterClassMap<ComparableRequestMap>();

                var comparables = new List<ComparableRequestViewModel>();
                while (csv.Read())
                {
                    var record = csv.GetRecord<ComparableRequestDto>();
                    comparables.Add(record.AsComparableRequest());
                }
                return comparables;
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error on importing Comparables..", exception);
                throw new GoSmartValueException("Error on importing Comparables..", exception);
            }
        }
        private IDictionary<int, List<string>> ValidateRawInstantValuationRecords(IEnumerable<ComparableRequestViewModel> comparableRequests)
        {
            var validationResults = new Dictionary<int, List<string>>();
            foreach (var request in comparableRequests.Select((value, index) => new { index, value }))
            {
                var validationResult = ValidateComparableRequest(request.value);
                if (validationResult != null)
                {
                    validationResults.Add(request.index, validationResult);
                }
            }

            return validationResults;
        }

        private List<string> ValidateComparableRequest(ComparableRequestViewModel request)
        {
            var result = _instantValuationValidators.Validate(request);
            if (!result.IsValid)
            {
                return ConvertToList(result.Errors);
            }

            return null;
        }

        private List<string> ConvertToList(IList<ValidationFailure> validationFailures)
        {
            return validationFailures.Select(failure => failure.ToString()).ToList();
        }

        private void SetLocationDetails(IEnumerable<Comparable> comparableResults)
        {
            var locations =
                _locationsQueries.FetchAllWhere(l => comparableResults.Select(c => c.LocationId).Contains(l.Id));
            var localities = _locationUow.LocalAreaRepository
                .Find(l => comparableResults
                    .Where(l => l.LocalityId.HasValue)
                    .Select(c => c.LocalityId).Contains(l.Id));
            foreach (var comparable in comparableResults)
            {
                Location location = null;
                if (comparable.LocationId > 0)
                {
                    location = locations?.FirstOrDefault(l => l.Id == comparable.LocationId);
                }

                if (location != null)
                {
                    comparable.LocationId = location.Id;

                    Locality locality = null;
                    if (comparable.LocalityId != null && comparable.LocalityId > 0)
                    {
                        if (comparable.LocalityId != null)
                            locality = localities.FirstOrDefault(l => l.Id == comparable.LocalityId.Value);
                    }

                    comparable.LocalityId = locality?.Id;
                    Street street = null;
                    if (!string.IsNullOrEmpty(comparable.StreetName) && locality != null)
                    {
                        street = _locationUow.AddStreetName(location.Id, locality.Id,
                            comparable.StreetName);
                    }

                    if (street != null)
                    {
                        comparable.StreetId = street?.Id;
                    }
                }
            }
        }

        private void SetLocationDetails(IEnumerable<ComparableRequestViewModel> comparableRequests)
        {
            var locations =
                _locationsQueries.FetchAllWhere(l => comparableRequests.Select(c => c.LocationId).Contains(l.Id));
            var localities = _locationUow.LocalAreaRepository
                .Find(l => comparableRequests
                    .Where(l => l.LocalityId.HasValue)
                    .Select(c => c.LocalityId).Contains(l.Id));
            foreach (var comparable in comparableRequests)
            {
                Location location = null;
                if (comparable.LocationId > 0)
                {
                    location = locations?.FirstOrDefault(l => l.Id == comparable.LocationId);
                }

                if (!string.IsNullOrEmpty(comparable.LocationName)
                    && comparable.LocationId == default)
                {
                    location = _locationUow.AddLocation(comparable.LocationName);
                }

                if (location != null)
                {
                    comparable.LocationId = location.Id;

                    Locality locality = null;
                    if (comparable.LocalityId != null && comparable.LocalityId > 0)
                    {
                        if (comparable.LocalityId != null)
                            locality = localities.FirstOrDefault(l => l.Id == comparable.LocalityId.Value);
                    }

                    if (!string.IsNullOrEmpty(comparable.LocalityName?.Trim()) && !comparable.LocalityId.HasValue)
                    {
                        locality = _locationUow.AddLocality(location.Id, comparable.LocalityName);
                    }

                    comparable.LocalityId = locality?.Id;
                    Street street = null;
                    if (!string.IsNullOrEmpty(comparable.StreetName) && locality != null)
                    {
                        street = _locationUow.AddStreetName(location.Id, locality.Id,
                            comparable.StreetName);
                    }

                    if (street != null)
                    {
                        comparable.StreetId = street?.Id;
                    }
                }
            }
        }


        public static IList<string> ValidateComparableRequest(IEnumerable<ComparableRequestViewModel> comparableRequestRequest)
        {
            IList<string> validationFailures = new List<string>();
            foreach (var comparable in comparableRequestRequest)
            {
                if (comparable.LocationId == 0)
                    validationFailures.Add($"No location set.");
                if (comparable.PropertyType == 0)
                    validationFailures.Add($"Property type must be specified.");
                if (string.IsNullOrEmpty(comparable.PlotNo))
                    validationFailures.Add($"Plot No must be specified.");
            }

            return validationFailures;
        }

        private async Task<ImportForInstantValuationResponse> CreateImportForInstantValuationResponse(
            IEnumerable<ComparableRequestViewModel> comparableRequests,
            IEnumerable<ComparableResultViewModel> comparableResults)
        {
            return await Task.Run(() => comparableRequests != null
                ? new ImportForInstantValuationResponse
                {
                    ComparableRequests = comparableRequests,
                    ComparablesResults = comparableResults,
                    Status = ImportStatus.Completed
                }
                : default);
        }

        public class ComparableRequestDto
        {
            public string LocationId { get; set; }
            public string LocationName { get; set; }
            public string LocalityId { get; set; }
            public string LocalityName { get; set; }
            public string StreetName { get; set; }
            public string PlotNo { get; set; }
            public string LandUse { get; set; }
            public string PropertyType { get; set; }
            public string Size { get; set; }
            public string PurchasePrice { get; set; }
            public string Source { get; set; }
            public string Host { get; set; }
            public string RequestUri { get; set; }
            public string RequestDate { get; set; }
            public string PurchaseDate { get; set; }
            public string PlotId { get; set; }
            public string StreetId { get; set; }


            public ComparableRequestViewModel AsComparableRequest()
            {
                return new ComparableRequestViewModel
                {
                    LocationName = this.LocationName,
                    LocalityName = this.LocalityName,
                    Size = ParseInt(this.Size) ?? 0,
                    PlotNo = this.PlotNo,
                    PropertyType = string.IsNullOrEmpty(this.PropertyType) ? Contracts.Enums.PropertyType.Vacant : Enum.Parse<PropertyType>(this.PropertyType),
                    LandUse = string.IsNullOrEmpty(this.LandUse) ? AV.Contracts.Enums.LandUse.Residential : Enum.Parse<LandUse>(this.LandUse),

                };
            }

            private static int? ParseInt(string value)
            {
                var success = int.TryParse(value, out var number);
                if (success)
                {
                    return number;
                }
                return null;
            }
        }

    }
}
