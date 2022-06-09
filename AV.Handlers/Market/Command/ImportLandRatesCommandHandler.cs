using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AV.Common.Entities;
using AV.Common.Interfaces;
using AV.Contracts.Enums;
using AV.Contracts.Models.Market;
using AV.Contracts.Models.Market.Commands;
using AV.Contracts.Models.Market.ResponseModels;
using AV.Handlers.Market.Mappers;
using AV.Handlers.Market.Validators;
using CsvHelper;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AV.Handlers.Market.Command
{
    public class ImportLandRatesCommandHandler : IRequestHandler<ImportLandRatesCommand, ImportLandRatesResponse>
    {
        private readonly IMarketInformationRepository _marketInformationRepository;
        private readonly ILogger _logger;
        private readonly LandRateValidator _landRateValidator;
        private readonly IMapper _mapper;

        public ImportLandRatesCommandHandler(
            IMarketInformationRepository marketInformationRepository,
            ILogger<ImportLandRatesCommandHandler> logger,
            LandRateValidator landRateValidator,
            IMapper mapper)
        {
            _marketInformationRepository = marketInformationRepository;
            _logger = logger;
            _landRateValidator = landRateValidator;
            _mapper = mapper;
        }

        public async Task<ImportLandRatesResponse> Handle(ImportLandRatesCommand command,
            CancellationToken cancellationToken)
        {
            var fileStream = ConvertFormFileToStream(command.CsvFormFile);
            var importHeader = new ImportHeader
            {
                FileName = command.CsvFormFile.FileName,
                FileStream = fileStream,
                Type = command.ImportType,
                ImportedById = command.ImportedBy,
                ActiveFrom = DateTimeOffset.UtcNow,
                ActiveTo = DateTimeOffset.UtcNow,
            };

            var landRates = ReadCsvFile(fileStream, importHeader);
            
            var result = ValidateLandRateRecords(landRates);
            if (result.Count > 0)
            {
                return new ImportLandRatesResponse
                {
                    Header = _mapper.Map<Contracts.Models.ImportHeader>(importHeader),
                    Data = _mapper.Map<IEnumerable<LandRateModel>>(landRates),
                    ImportErrors = result,
                    Status = ImportStatus.FailedValidation
                };
            }

            // CalculateComprables
            var importedLandRates = await _marketInformationRepository.AddLandRates(landRates, cancellationToken);
            return CreateImportLandRatesResponse(importedLandRates);
        }

        private IDictionary<int,List<string>> ValidateLandRateRecords(IEnumerable<LandRate> landRates)
        {
            var validationResults = new Dictionary<int, List<string>>();
            foreach (var landRate in landRates.Select((value, index) => new {index, value}))
            {
                var validationResult = ValidateLandRateRecord(landRate.value );
                if (validationResult != null)
                {
                    validationResults.Add(landRate.index, validationResult);
                }
            }

            return validationResults;
        }

        private List<string> ValidateLandRateRecord(LandRate landRate)
        {
            var result = _landRateValidator.Validate(landRate);
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

        private ImportLandRatesResponse CreateImportLandRatesResponse(IEnumerable<LandRate> importedLandRates)
        {
            return importedLandRates != null
                ? new ImportLandRatesResponse
                {
                    Header = _mapper.Map<Contracts.Models.ImportHeader>(importedLandRates.FirstOrDefault().ImportHeader),
                    Data = _mapper.Map<IEnumerable<LandRateModel>>(importedLandRates),
                    Status = ImportStatus.Completed
                }
                : default;
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

        private IEnumerable<LandRate> ReadCsvFile(byte[] fileByteArray, ImportHeader importHeader)
        {
            // Read the file and display it line by line.  
            try
            {
                using var fileStream = new MemoryStream(fileByteArray);
                using TextReader reader = new StreamReader(fileStream);
                using var csv = new CsvReader(reader, CultureInfo.CurrentCulture);

                csv.Context.RegisterClassMap<LandRateMap>();
                //var records = csv.GetRecords<LandRate>();
                var landRates = new List<LandRate>();
                while (csv.Read())
                {
                    var record = csv.GetRecord<LandRateDto>();
                    landRates.Add(record.AsLandRate());
                }
                return landRates;
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error on importing Land Rates..", exception);
                throw new GoSmartValueException("Error on importing Land Rates..", exception);
            }
        }

        public class LandRateDto
        {
            public string CountryId { get; set; }
            public string DistrictId { get; set; }
            public string LocationId { get; set; }
            public string LocalityId { get; set; }
            public string Metric { get; set; }
            public string Rate { get; set; }
            //Calculated from verified comparables on record creation
            public string LowIncome { get; set; }
            public string MiddleIncome { get; set; }
            public string HighIncome { get; set; }
            public string Zoning { get; set; }
            public string AveragePrice { get; set; }

            public LandRate AsLandRate()
            {
                return new LandRate
                {
                    CountryId = ParseInt(this.CountryId) ?? 1,
                    DistrictId = ParseInt(this.DistrictId),
                    LocationId = ParseInt(this.LocationId) ?? 0,
                    LocalityId = ParseInt(this.LocalityId),
                    Metric = string.IsNullOrEmpty(this.Metric) ? Contracts.Enums.Metric.SquareMetres : Enum.Parse<Metric>(this.Metric),
                    Zoning = Enum.Parse<Zoning>(this.Zoning),
                    Rate = ParseDecimal(this.Rate),
                    LowIncome = ParseInt(this.LowIncome) ?? 0,
                    MiddleIncome = ParseInt(this.MiddleIncome) ?? 0,
                    HighIncome = ParseInt(this.HighIncome) ?? 0,
                    AveragePrice = ParseDecimal(this.HighIncome),
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

            private static decimal? ParseDecimal(string value)
            {
                var success = decimal.TryParse(value, out var number);
                if (success)
                {
                    return number;
                }
                return null;
            }
        }
    }
}