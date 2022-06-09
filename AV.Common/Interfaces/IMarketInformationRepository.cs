using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AV.Common.Entities;
using AV.Contracts.Enums;

namespace AV.Common.Interfaces
{
    public interface IMarketInformationRepository : IRepository<MarketInformation>
    {
        Task<MarketInformation> GetMarketInformation(Zoning zoning, int locationId, int? localityId, CancellationToken cancellationToken = new CancellationToken());
        Task<ICollection<MarketInformation>> GetAllMarketInformationAsync(CancellationToken cancellationToken);
        Task<MarketInformation> AddAsync(MarketInformation marketInfo, CancellationToken cancellationToken);
        Task<IEnumerable<LandRate>> GetLandRates(CancellationToken cancellationToken);
        Task<IEnumerable<LandRate>> AddLandRates(IEnumerable<LandRate> landRates, CancellationToken cancellationToken);
        Task<IEnumerable<BuildingCost>>  GetBuildingCosts(CancellationToken cancellationToken);
        Task<IEnumerable<BuildingMaterialCost>> GetBuildingMaterialCosts(CancellationToken cancellationToken);
		Task<IEnumerable<BuildingCost>> AddBuildingCosts(IEnumerable<BuildingCost> buildingCosts, CancellationToken cancellationToken);
        Task<IEnumerable<Comparable>> PropertySearch(PropertySearchType searchType ,int locationId, string plotNo, decimal plotSize, CancellationToken cancellationToken);
        Task<IEnumerable<BuildingMaterialCost>> AddBuildingMaterialCosts(IEnumerable<BuildingMaterialCost> buildingMaterialCosts, CancellationToken cancellationToken);
        Task<Comparable> GetPropertySearchDetails(Guid id);
    }
}
