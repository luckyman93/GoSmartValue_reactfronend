using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AV.Common.DTOs;
using AV.Common.Entities;
using AV.Contracts.Enums;
using AV.Contracts.Models.Market.Requests;
using AV.Contracts.Models.Market.ResponseModels;
using AV.Contracts.Models.Valuation;
using GoSmartValue.Web.Models;

namespace GoSmartValue.Web.Services
{
    public interface IValuationsService
    {
        ICollection<LocationViewModel> GetLocations();
        Task<CountryViewModel> GetCountry(int countryId);
        LocationViewModel GetLocationByName(string locationName);
        LocalityViewModel GetLocalArea(int localAreaId);
        ICollection<LocalityViewModel> GetLocalities(int? localAreaId = null);
        Task<(ComparableResultViewModel, IList<string>)> CalculateValuation(ComparableRequestViewModel comparableRequest);
        Task<(List<ComparableResultViewModel>, IList<string>)> CalculateValuation(List<ComparableRequestViewModel> comparableRequest);
        ICollection<StreetViewModel> GetStreets(int localityId);
        ICollection<LocationViewModel> GetAllLocationData();
        ICollection<ComparableViewModel> GetAllValuations(DataState? dataState = null);
        ComparableViewModel GetValuation(Guid id);
        Task DeleteComparable(Guid id);
        Task VerifyComparable(Guid id);
        void RequestFullReport(ComparableResultViewModel comparableResult);
        void AddPropertyRecord(ComparableViewModel comparableViewModel);
        Task<ComparableResultViewModel> GetComparableResult(Guid comparableResultId);
        void SavePropertyRecord(ComparableViewModel propertySaleView);
        LocationViewModel AddLocation(string locationName);
        LocationViewModel AddLocation(LocationViewModel location);
        LocalityViewModel AddLocality(int locationNAme, string localityName);
        LocalityViewModel AddLocality(LocalityViewModel locality);
        ValuationResultViewModel ProcessValuation(User currentUser, ValuationModel valuation);
        ValuationResultViewModel GetValuationResult(User requester, Guid instructionId);
        ValidationResult ValidateValuationAcceptance(ValuationResultViewModel acceptanceRequest, User currentUser);
        Task AcceptValuation(ValuationResultViewModel acceptanceRequest);
        Task<PropertySearchResponse> PropertySearch(PropertySearchRequest request, CancellationToken cancellationToken);
        Task<Comparable> GetPropertySearchDetails(Guid id);

        DetailedReportViewModel GetStandardValuationReport(Guid instructionId);
        Task<InstantReportViewModel> PerformStandardUserComparable(Guid comparableId);
        IList<GetInstantReportPreView> GetInstantReportPreView(Guid userId);
    }
}