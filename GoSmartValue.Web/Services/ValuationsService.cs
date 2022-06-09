using AutoMapper;
using AV.Common;
using AV.Common.DTOs;
using AV.Common.Entities;
using AV.Common.Interfaces;
using AV.Common.Interfaces.Repositories;
using AV.Common.Interfaces.Services;
using AV.Common.Interfaces.UnitOfWorks;
using AV.Contracts.Enums;
using AV.Contracts.Models;
using AV.Contracts.Models.Market.Requests;
using AV.Contracts.Models.Market.ResponseModels;
using AV.Contracts.Models.Valuation;
using AV.Contracts.Services;
using GoSmartValue.Web.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ComparableRequestViewModel = AV.Contracts.Models.Valuation.ComparableRequestViewModel;
using Locality = AV.Common.Entities.Locality;
using Location = AV.Common.Entities.Location;
using Street = AV.Common.Entities.Street;
using User = AV.Common.Entities.User;

namespace GoSmartValue.Web.Services
{
    public class ValuationsService : IValuationsService
    {
        private readonly ILocationUnitOfWork _locationUow;
        private readonly IValuationsUnitOfWork _valuationsUnitOfWork;
        private readonly IComparableUnitOfWork _comparableUnit;
        private readonly IInstructionUoW _instructionUoW;
        private readonly IOptions<SmtpConfiguration> _smtpOptions;
        private readonly IEmailService _emailService;
        private readonly IDocumentStoreService _documentStoreService;
        private readonly IMarketInformationRepository _marketInformationRepository;
        private readonly IDocumentService _documentService;
        private readonly IMapper _mapper;
        private readonly IUserManagerUnitOfWork _userUow;
        private readonly IComparableRepository _comparambleService;


        public ValuationsService(
            ILocationUnitOfWork locationUoW,
            IValuationsUnitOfWork valuationsUnitOfWork,
            IComparableUnitOfWork comparableUnit,
            IInstructionUoW instructionUoW,
            IOptions<SmtpConfiguration> smtpOptions,
            IEmailService emailService,
            IDocumentStoreService documentStoreService,
            IMarketInformationRepository marketInformationRepository,
            IDocumentService documentService,
            IMapper mapper,
            IUserManagerUnitOfWork userUow,
            IComparableRepository comparambleService

            )
        {
            _locationUow = locationUoW;
            _valuationsUnitOfWork = valuationsUnitOfWork;
            _comparableUnit = comparableUnit;
            _instructionUoW = instructionUoW;
            _smtpOptions = smtpOptions;
            _emailService = emailService;
            _documentStoreService = documentStoreService;
            _marketInformationRepository = marketInformationRepository;
            _documentService = documentService;
            _mapper = mapper;
            _userUow = userUow;
            _comparambleService = comparambleService;

        }

        public ICollection<LocationViewModel> GetLocations()
        {
            var locations = _locationUow.GetAllLocations();
            return _mapper.Map<ICollection<LocationViewModel>>(locations);
        }

        public async Task<CountryViewModel> GetCountry(int countryId)
        {
            var country = await _locationUow.GetCountry(countryId);
            return _mapper.Map<CountryViewModel>(country);
        }

        public LocationViewModel AddLocation(string locationName)
        {
            var location = _locationUow.AddLocation(locationName);
            return _mapper.Map<LocationViewModel>(location);
        }

        public LocationViewModel AddLocation(LocationViewModel location)
        {
            return _mapper.Map<LocationViewModel>(_locationUow.AddLocation(location.Name));
        }

        public ICollection<LocationViewModel> GetAllLocationData()
        {
            var locations = _locationUow.GetAllLocationData();
            return _mapper.Map<ICollection<LocationViewModel>>(locations);
        }

        public ICollection<ComparableViewModel> GetAllValuations(DataState? dataState)
        {
            var valuations = _comparableUnit.GetAllComparables(dataState);
            var propSales = new List<ComparableViewModel>();
            foreach (var propertySale in valuations)
            {
                propSales.Add(ExtractBuyerAndSeller(propertySale, _mapper));
            }
            return propSales;
        }

        public ValuationResultViewModel ProcessValuation(User currentUser, ValuationModel valuation)
        {
            valuation.ValuerId = currentUser.Id;
            var valuationEntity = _mapper.Map<Valuation>(valuation);
            var result = _valuationsUnitOfWork.ProcessValuation(valuationEntity);

            result.Instruction = result.Valuation.Instruction;
            result.Valuation.Instruction = null;

            var returnValuationResult = _mapper.Map<ValuationResultViewModel>(result);

            returnValuationResult.Valuation.Instruction = returnValuationResult.Instruction;
            returnValuationResult.InstructionId = result.Instruction.Id;

            return returnValuationResult;
        }

        private static ComparableViewModel ExtractBuyerAndSeller(Comparable propertySale, IMapper mapper)
        {
            var prop = mapper.Map<ComparableViewModel>(propertySale);
            if (propertySale?.Seller != default)
            {
                prop.SellerId = propertySale.Seller.Id;
                prop.SellerFirstName = propertySale.Seller.FirstName;
                prop.SellerLastName = propertySale.Seller.LastName;
                prop.SellerMobileNumber = propertySale.Seller.MobileNumber;
                prop.SellerEmail = propertySale.Seller.Email;
                prop.SellerIdentityNumber = propertySale.Seller.IdentityNumber;
            }

            if (propertySale?.Buyer == default)
                return prop;
            prop.BuyerId = propertySale.Buyer.Id;
            prop.BuyerFirstName = propertySale.Buyer.FirstName;
            prop.BuyerLastName = propertySale.Buyer.LastName;
            prop.BuyerMobileNumber = propertySale.Buyer.MobileNumber;
            prop.BuyerEmail = propertySale.Buyer?.Email;
            prop.BuyerIdentityNumber = propertySale.Buyer.IdentityNumber;

            return prop;
        }

        public ComparableViewModel GetValuation(Guid id)
        {
            var valuation = _comparableUnit.GetComparable(id);
            return ExtractBuyerAndSeller(valuation, _mapper);
        }

        public async Task DeleteComparable(Guid id)
        {
            await _comparableUnit.SoftDelete(id);
        } 
        
        public async Task VerifyComparable(Guid id)
        {
            await _comparableUnit.VerifyComparable(id);
        }

        public void RequestFullReport(ComparableResultViewModel comparableResult)
        {
            var reportRequest = _mapper.Map<ReportRequest>(comparableResult);
            if (comparableResult?.PropertyFeatures != null && comparableResult.PropertyFeatures.Any())
            {
                var features = new List<PropertyFeature>();
                foreach (var feature in comparableResult.PropertyFeatures)
                {
                    features.Add(new PropertyFeature
                    {
                        FeatureType = feature
                    });
                }
                reportRequest.Features = features;
            }
            _comparableUnit.RequestFullReport(reportRequest);
        }

        public void AddPropertyRecord(ComparableViewModel comparableViewModel)
        {
            var property = _mapper.Map<Comparable>(comparableViewModel);
            _comparableUnit.AddProperty(property);
        }

        public async Task<ComparableResultViewModel> GetComparableResult(Guid comparableResultId)
        {
            var comparableResult = await _comparableUnit.GetComparableResult(comparableResultId);
            var result = _mapper.Map<ComparableResultViewModel>(comparableResult);
            result.Comparable = _mapper.Map<ComparableViewModel>(comparableResult.Comparable);
            return result;
        }

        public void SavePropertyRecord(ComparableViewModel propertySaleView)
        {
            var property = _mapper.Map<Comparable>(propertySaleView);
            property.Seller = ExtractSeller(propertySaleView);
            property.Buyer = ExtractBuyer(propertySaleView);
            _comparableUnit.SaveComparableChanges(property);
        }

        public LocationViewModel GetLocationByName(string locationName)
        {
            var location = _locationUow.GetAllLocations()
                .SingleOrDefault(l => l.Name.Equals(locationName.Trim(), StringComparison.CurrentCultureIgnoreCase));
            if (location == null)
            {
                location = _locationUow.AddLocation(locationName.Trim());
            }
            return _mapper.Map<LocationViewModel>(location);
        }

        public LocalityViewModel GetLocalArea(int localAreaId)
        {
            var localAreas = _locationUow.GetLocalArea(localAreaId);
            return _mapper.Map<LocalityViewModel>(localAreas);
        }

        public LocalityViewModel AddLocality(int locationId, string localityName)
        {
            var localAreas = _locationUow.AddLocality(locationId, localityName);
            return _mapper.Map<LocalityViewModel>(localAreas);
        }

        public LocalityViewModel AddLocality(LocalityViewModel locality)
        {
            return _mapper.Map<LocalityViewModel>(_locationUow.AddLocality(locality.LocationId, locality.Name));
        }

        public ValuationResultViewModel GetValuationResult(User requester, Guid instructionId)
        {
            var valuation = _valuationsUnitOfWork.GetValuationByInstructionId(instructionId);

            valuation.Instruction ??= _instructionUoW.GetInstruction(requester, instructionId);
            return new ValuationResultViewModel
            {
                Valuation = _mapper.Map<ValuationModel>(valuation),
                ValuationId = valuation.Id,
                Instruction = _mapper.Map<InstructionModel>(valuation.Instruction),
                InstructionId = instructionId,
                Comparable = _mapper.Map<ComparableViewModel>(valuation.ComparableResult?.Comparable),
                Comparables = _mapper.Map<ICollection<ComparableViewModel>>(valuation.ComparableResult?.Comparables?.Select(c => c.Comparable).ToList()),
                Valuer = _mapper.Map<AV.Contracts.Models.Users.UserModel>(valuation.Valuer),
                ValuerId = valuation.ValuerId
            };
        }

        public DetailedReportViewModel GetStandardValuationReport(Guid instructionId)
        {
            var result = new DetailedReportViewModel();
            var valuation = _valuationsUnitOfWork.GetValuationByInstructionId(instructionId);

            //set valuation record
            result.ValuationReport = _mapper.Map<ValuationReportViewModel>(valuation);

            //set valuer 
            var user = _userUow.GetUser(valuation.ValuerId);
            result.valuer = _mapper.Map<DetailedReportValuer>(user);

            //set comparables 
            result.Comparables = valuation.ComparableResult?.Comparables?.Select(c => new ReportComparablesViewModel
            {
                Buyer = c.Comparable?.BuyerName,
                DateOfSale = c.Comparable?.DateOfSale != null ? c.Comparable?.DateOfSale.Value : null,
                PlotNumber = c.Comparable?.PlotNo,
                PlotSize = c.Comparable?.PlotSize,
                Seller = c.Comparable?.SellerName,
                Transaction = c.Comparable?.SalePrice
            }).ToList();

            return result;
        }

        public ValuationResultViewModel GetValuationResult(Guid instructionId)
        {
            var valuation = _valuationsUnitOfWork.GetValuationByInstructionId(instructionId);

            if (valuation.Instruction == null)
            {
                valuation.Instruction = _instructionUoW.GetInstruction(instructionId);
            }
            return new ValuationResultViewModel
            {
                Valuation = _mapper.Map<ValuationModel>(valuation),
                ValuationId = valuation.Id,
                Instruction = _mapper.Map<InstructionModel>(valuation.Instruction),
                InstructionId = instructionId,
                Comparable = _mapper.Map<ComparableViewModel>(valuation.ComparableResult?.Comparable),
                Comparables = _mapper.Map<ICollection<ComparableViewModel>>(valuation.ComparableResult?.Comparables?.Select(c => c.Comparable).ToList()),
                Valuer = _mapper.Map<AV.Contracts.Models.Users.UserModel>(valuation.Valuer),
                ValuerId = valuation.ValuerId
            };
        }

        public ICollection<LocalityViewModel> GetLocalities(int? localAreaId = null)
        {
            var localAreas = _locationUow.GetLocalAreas(localAreaId);
            return _mapper.Map<ICollection<LocalityViewModel>>(localAreas);
        }

        public ICollection<StreetViewModel> GetStreets(int localityId)
        {
            var localAreas = _locationUow.GetStreets(localityId);
            return _mapper.Map<ICollection<StreetViewModel>>(localAreas);
        }

        public async Task<(ComparableResultViewModel, IList<string>)> CalculateValuation(ComparableRequestViewModel comparableRequest)
        {
            SetLocationDetails(comparableRequest);

            var validationResults = ValidateComparableRequest(comparableRequest);

            if (validationResults.Count == 0)
            {
                var comparable = _mapper.Map<Comparable>(comparableRequest);
                //Performs Comparable
                var comparableResults = await _comparableUnit.PerformComparable(comparable);
                //
                return (new ComparableResultViewModel
                {
                    EstimatedValue = comparableResults.EstimatedValue,
                    ComparableResultId = comparableResults.ReferenceNumber,
                    ReferenceNumber = comparableResults.ReferenceNumber,
                    ComparableId = comparableResults.ComparableId,
                    ComparableRequest = comparableRequest,
                    ReportRequested = false
                }, new List<string>());
            }
            return (null, validationResults);
        }

        private void SetLocationDetails(ComparableRequestViewModel comparableRequest)
        {
            Location location = null;
            if (comparableRequest.LocationId > 0)
            {
                location = _locationUow.GetLocation(comparableRequest.LocationId);
            }

            if (!string.IsNullOrEmpty(comparableRequest.LocationName)
                && comparableRequest.LocationId == default)
            {
                location = _locationUow.AddLocation(comparableRequest.LocationName);
            }

            if (location != null)
            {
                comparableRequest.LocationId = location.Id;

                Locality locality = null;
                if (comparableRequest.LocalityId != null && comparableRequest.LocalityId > 0)
                {
                    if (comparableRequest.LocalityId != null)
                        locality = _locationUow.GetLocalArea(comparableRequest.LocalityId.Value);
                }

                if (!string.IsNullOrEmpty(comparableRequest.LocalityName?.Trim()) && !comparableRequest.LocalityId.HasValue)
                {
                    locality = _locationUow.AddLocality(location.Id, comparableRequest.LocalityName);
                }

                comparableRequest.LocalityId = locality?.Id;
                Street street = null;
                if (!string.IsNullOrEmpty(comparableRequest.StreetName) && locality != null)
                {
                    street = _locationUow.AddStreetName(location.Id, locality.Id,
                        comparableRequest.StreetName);
                }

                if (street != null)
                {
                    comparableRequest.StreetId = street?.Id;
                }
            }
        }
        private void SetLocationDetails(List<ComparableRequestViewModel> comparableRequest)
        {
            foreach (var comparable in comparableRequest)
            {
                Location location = null;
                if (comparable.LocationId > 0)
                {
                    location = _locationUow.GetLocation(comparable.LocationId);
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
                            locality = _locationUow.GetLocalArea(comparable.LocalityId.Value);
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

        private IList<string> ValidateComparableRequest(ComparableRequestViewModel comparableRequestRequest)
        {
            IList<string> validationFailures = new List<string>();
            if (comparableRequestRequest.LocationId == 0)
                validationFailures.Add($"No location set.");
            if (comparableRequestRequest.PropertyType == 0)
                validationFailures.Add($"Property type must be specified.");
            if (string.IsNullOrEmpty(comparableRequestRequest.PlotNo))
                validationFailures.Add($"Plot No must be specified.");

            return validationFailures;
        }
        private static IList<string> ValidateComparableRequest(List<ComparableRequestViewModel> comparableRequestRequest)
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

        private static Seller ExtractSeller(ComparableViewModel propertySaleView)
        {
            return new Seller
            {
                FirstName = propertySaleView.SellerFirstName,
                LastName = propertySaleView.SellerLastName,
                Email = propertySaleView.SellerEmail,
                MobileNumber = propertySaleView.SellerMobileNumber,
                IdentityNumber = propertySaleView.SellerIdentityNumber,
                Salutation = propertySaleView.SellerSalutation,
            };
        }

        private static Buyer ExtractBuyer(ComparableViewModel propertySaleView)
        {
            return new Buyer
            {
                FirstName = propertySaleView.BuyerFirstName,
                LastName = propertySaleView.BuyerLastName,
                Email = propertySaleView.BuyerEmail,
                MobileNumber = propertySaleView.BuyerMobileNumber,
                IdentityNumber = propertySaleView.BuyerIdentityNumber,
                Salutation = propertySaleView.BuyerSalutation,
            };
        }

        public ValidationResult ValidateValuationAcceptance(ValuationResultViewModel acceptanceRequest, User currentUser)
        {
            var result = new ValidationResult();
            if (!acceptanceRequest.Accepted)
            {
                result.AddMessage("Result must be accepted.");
            }

            if (acceptanceRequest.ValuerId != currentUser.Id)
            {
                result.AddMessage("You are not authorized to accept this valuation.");
            }

            if (!acceptanceRequest.Valuation.Value.HasValue || !acceptanceRequest.Valuation.EstimatedValue.HasValue)
            {
                if (!acceptanceRequest.Valuation.EstimatedValue.HasValue)
                    result.AddMessage($"Estimated value: {acceptanceRequest.Valuation.EstimatedValue} cannot be 0");

                if (!acceptanceRequest.Valuation.Value.HasValue)
                    result.AddMessage($"Adjusted value: {acceptanceRequest.Valuation.EstimatedValue} cannot be 0");
            }

            if (acceptanceRequest.Valuation.ServiceFee == (decimal)0.00)
            {
                result.AddMessage("Service Fee Cannot be zero.");
            }

            //if (!AdjustedAmountWithinTenPercent(acceptanceRequest.Valuation.Value ?? 0, acceptanceRequest.Valuation.EstimatedValue ?? 0))
            //{
            //    result.AddMessage("The adjusted amount cannot be more than 10% from the estimated value.");
            //}
            return result;
        }

        public async Task AcceptValuation(ValuationResultViewModel acceptanceRequest)
        {
            if (acceptanceRequest.ValuationId == null || !acceptanceRequest.Valuation.Value.HasValue)
                return;

            _valuationsUnitOfWork.AcceptValuation(acceptanceRequest.ValuationId.Value,
            acceptanceRequest.Valuation.Value.Value,
            acceptanceRequest.Valuation.ServiceFee,
            acceptanceRequest.Valuation.AdjustmentReason);

            //Trigger Report Generation
            await SendValuationAcceptedEmail(acceptanceRequest);
        }

        private async Task SendValuationAcceptedEmail(ValuationResultViewModel acceptanceRequest)
        {
            var data = new EmailTemplate
            {
                Data = new Dictionary<string, string>
                {
                    {"clientName", GetClientName(acceptanceRequest)},
                    {"valuerName", $"{acceptanceRequest.Valuer.FirstName} {acceptanceRequest.Valuer.LastName}"},
                    {"dashboardLink", "www.gosmartvalue.com/index"}
                },
                Template = TemplateConstants.TemplateValuationAcceptedValuer
            };

            IEnumerable<EmailAttachment> attachments = null;
            //if (acceptanceRequest.ValuationId != null)
            //{
            //    var attachment = await GenerateValuationReport(acceptanceRequest.ValuationId.Value);
            //    if (attachment != null)
            //    {
            //        attachments = new List<EmailAttachment>() { attachment };
            //    }
            //}

            await _emailService.SendMail(acceptanceRequest.Instruction.Issuer.UserName,
                $"Valuation for report is now ready",
                TemplateConstants.TemplateCompleteValuationIssuer,
                _smtpOptions.Value,
                acceptanceRequest.Valuer.UserName,
                data, attachments);
        }

        public async Task<(List<ComparableResultViewModel>, IList<string>)> CalculateValuation(List<ComparableRequestViewModel> comparableRequest)
        {
            SetLocationDetails(comparableRequest);

            var validationResults = ValidateComparableRequest(comparableRequest);
            List<ComparableResultViewModel> comparablesList = new List<ComparableResultViewModel>();
            if (validationResults.Count == 0)
            {
                foreach (var comparable in comparableRequest)
                {
                    var comp = _mapper.Map<Comparable>(comparable);
                    //Performs Comparable
                    var comparableResults = await _comparableUnit.PerformComparable(comp);
                    //
                    comparablesList.Add(new ComparableResultViewModel
                    {
                        EstimatedValue = comparableResults.EstimatedValue,
                        ComparableResultId = comparableResults.ReferenceNumber,
                        ReferenceNumber = comparableResults.ReferenceNumber,
                        ComparableId = comparableResults.ComparableId,
                        ComparableRequest = comparable,
                        Comparables = _mapper.Map<List<ComparableViewModel>>(comparableResults?.Comparables?.ToList()),
                        ReportRequested = false
                    });
                }
                return (comparablesList, new List<string>());
            }
            return (null, validationResults);
        }

        private IDictionary<string, string> ConvertValuationResult(ValuationResultViewModel valuationResult)
        {
            var dataDictionary = new Dictionary<string, string>();
            dataDictionary.Add("valuerName", $"{valuationResult.Valuer?.FirstName} {valuationResult.Valuer?.LastName}");
            dataDictionary.Add("propertyUse", valuationResult.Comparable?.LandUse.ToString());
            dataDictionary.Add("plotNumber", valuationResult.Instruction.PlotNumber);
            dataDictionary.Add("locality", valuationResult.Instruction?.LocalityName);
            dataDictionary.Add("location", valuationResult.Instruction?.LocationName);
            dataDictionary.Add("clientName", valuationResult.Instruction?.ClientName);
            dataDictionary.Add("clientContact", valuationResult.Instruction?.ClientMobileNumber);
            dataDictionary.Add("valuationRequester", valuationResult.Instruction?.Issuer.ToString());
            //dataDictionary.Add("purposeOfValuation",);
            dataDictionary.Add("propertyToBeValued", $"{valuationResult.Comparable?.LandUse.ToString()},{valuationResult.Instruction.PlotNumber},{valuationResult.Instruction?.LocalityName},{valuationResult.Instruction?.LocationName}");
            dataDictionary.Add("plotSize", valuationResult.Valuation?.PlotSize.ToString("0.00"));
            dataDictionary.Add("valuationDate", (valuationResult.Valuation?.ValuationDate).ToString());
            //dataDictionary.Add("ValuerComments",);
            dataDictionary.Add("title", valuationResult.Valuation?.TitleDeedNo);
            dataDictionary.Add("zoning", valuationResult.Valuation?.Zoning.ToString());
            dataDictionary.Add("amenities", valuationResult.Valuation?.Amenities.ToString());
            dataDictionary.Add("marketCommentary", valuationResult.Valuation?.MarketCommentary);
            dataDictionary.Add("comparables", valuationResult.Comparables.ToString());
            dataDictionary.Add("averagePropertyValue", (valuationResult.Valuation?.EstimatedValue).ToString());
            // dataDictionary.Add("valuerReac", valuationResult.Valuer.ReacNumber);


            return dataDictionary;
        }

        public async Task<PropertySearchResponse> PropertySearch(PropertySearchRequest request, CancellationToken cancellationToken)
        {
            var compRequest = new ComparableRequestViewModel
            {
                LocationName = request.LocationName,
                PlotNo = request.PlotNo,
                Size = int.Parse(request.Size),
                PlotSize = int.Parse(request.Size)
            };

            SetLocationDetails(compRequest);

            var comparable = _mapper.Map<Comparable>(compRequest);

            var propertyInfo = await _marketInformationRepository.PropertySearch(request.PropertySearchType, comparable.LocationId, comparable.PlotNo, comparable.PlotSize, cancellationToken);
            if (propertyInfo != null)
                return new PropertySearchResponse
                {
                    PropertyList = MapComparable(propertyInfo),
                };
            return new PropertySearchResponse();
        }
        public async Task<Comparable> GetPropertySearchDetails(Guid id)
        {
            var propertyInfo = await _marketInformationRepository.GetPropertySearchDetails(id);
            return propertyInfo;
        }
        private string GetClientName(ValuationResultViewModel acceptanceRequest)
        {
            // self instructed valuation
            if (acceptanceRequest.Instruction.IssuerId == acceptanceRequest.ValuerId)
            {
                return $"{acceptanceRequest.Instruction.ClientName}";
            }

            return $"{acceptanceRequest.Instruction.Issuer.FirstName} {acceptanceRequest.Instruction.Issuer.LastName}";
        }

        private static bool AdjustedAmountWithinTenPercent(decimal adjusted, decimal estimatedValue)
        {
            if (adjusted <= 0 || estimatedValue <= 0)
                return false;
            return Math.Abs((adjusted - estimatedValue) / estimatedValue * 100) <= 10;
        }

        private IEnumerable<ComparableViewModel> MapComparable(IEnumerable<Comparable> propertyInfo)
        {
            var PropertyInfoModels = new List<ComparableViewModel>();
            foreach (var info in propertyInfo)
            {
                var PropertyInfoModel = new ComparableViewModel();
                PropertyInfoModel.Location = _mapper.Map<AV.Contracts.Models.Location>(info.Location);
                PropertyInfoModel.Locality = _mapper.Map<AV.Contracts.Models.Locality>(info.Locality);
                PropertyInfoModel.LandUse = info.LandUse;
                PropertyInfoModel.BandClass = _mapper.Map<ComparableBandSizeViewModel>(info.BandClass);
                PropertyInfoModel.BankName = info.BankName;
                PropertyInfoModel.BondAmount = info.BondAmount;
                PropertyInfoModel.BondNumber = info.BondNumber;
                PropertyInfoModel.Buyer = _mapper.Map<BuyerModel>(info.Buyer);
                PropertyInfoModel.BuyerName = info.BuyerName;
                PropertyInfoModel.Seller = _mapper.Map<SellerModel>(info.Seller);
                PropertyInfoModel.TitleDeedNo = info.TitleDeedNo;
                PropertyInfoModel.DateOfSale = info.DateOfSale;
                PropertyInfoModel.Features = _mapper.Map<ICollection<PropertyFeatureModel>>(info.Features);
                PropertyInfoModel.Longitude = info.Longitude;
                PropertyInfoModel.Metric = info.Metric;
                PropertyInfoModel.PlotNo = info.PlotNo;
                PropertyInfoModel.Latitude = info.Latitude;
                PropertyInfoModel.PlotSize = info.PlotSize;
                PropertyInfoModel.PropertyType = info.PropertyType;
                PropertyInfoModel.Rooms = _mapper.Map<ICollection<RoomsModel>>(info.Rooms);
                PropertyInfoModel.SalePrice = info.SalePrice ?? 0;
                PropertyInfoModel.StreetName = info.StreetName;
                PropertyInfoModel.TransactionType = info.TransactionType;
                PropertyInfoModel.LocalityId = info.LocalityId;
                PropertyInfoModel.LocationId = info.LocationId;
                PropertyInfoModel.Id = info.Id;

                PropertyInfoModels.Add(PropertyInfoModel);
            }

            return PropertyInfoModels;
        }

        public async Task<InstantReportViewModel> PerformStandardUserComparable(Guid comparableId)
        {

            var comparable = await _comparambleService.GetComparable(comparableId);
            if (comparable is not { PaymentStatus: PaymentStatus.Paid })
            {
                return null;
            }

            var comparableResult = await _comparambleService.PerformComparable(comparable);
            var result = _mapper.Map<InstantReportViewModel>(comparableResult);

            result.Comparables = comparableResult.Comparables?.Select(c => new ReportComparablesViewModel
            {
                Buyer = c.Comparable.BuyerName,
                DateOfSale = c.Comparable.DateOfSale.Value,
                PlotNumber = c.Comparable.PlotNo,
                PlotSize = c.Comparable.PlotSize,
                Seller = c.Comparable.SellerName,
                Transaction = c.Comparable.SalePrice
            }).ToList();

            return result;
        }

        public IList<GetInstantReportPreView> GetInstantReportPreView(Guid userId)
        {
            var comparables = _comparambleService.GetComparablesByAddedBy(userId);
            return comparables.Select(c => _mapper.Map<GetInstantReportPreView>(c)).ToList();

        }
    }
}