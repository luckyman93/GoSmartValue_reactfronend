using AV.Common.Entities;
using AV.Common.Interfaces;
using AV.Contracts.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AV.Persistence.EntityFramework.Repositories
{
    public class MarketInformationRepository : Repository<MarketInformation>, IMarketInformationRepository
    {
        public MarketInformationRepository(ValuationsContext context) : base(context)
        {
        }

        public async Task<MarketInformation> GetMarketInformation(Zoning zoning, int locationId, int? localityId, CancellationToken cancellationToken = new CancellationToken())
        {
            var result = DbContext.Set<MarketInformation>()
                .Include(l => l.Locality)
                .Where(l => l.LocationId == locationId
                            && l.Zoning == zoning);
            if (localityId.HasValue)
            {
                result.Where(l => l.LocalityId == localityId.Value);
            }
            return await result.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<ICollection<MarketInformation>> GetAllMarketInformationAsync(CancellationToken cancellationToken)
        {
            return await DbContext.Set<MarketInformation>()
                .AsNoTracking()
                .Include(m => m.Location)
                .Include(m => m.Locality)
                .Include(m => m.District)
                .ToListAsync(cancellationToken);
        }

        public async Task<MarketInformation> AddAsync(MarketInformation marketInfo, CancellationToken cancellationToken)
        {
            var existingMarketInfo = await DbContext.Set<MarketInformation>()
                                        .FirstOrDefaultAsync(m => m.CountryId == marketInfo.CountryId
                                                                             && m.DistrictId == marketInfo.DistrictId
                                                                             && m.LocationId == marketInfo.LocationId
                                                                             && m.LocalityId == marketInfo.LocalityId, cancellationToken: cancellationToken);

            if (existingMarketInfo != default)
            {
                existingMarketInfo = marketInfo;
                DbContext.Set<MarketInformation>().Update(existingMarketInfo);
                await DbContext.Entry(existingMarketInfo).ReloadAsync(cancellationToken);
                await SaveChangesAsync();
                return existingMarketInfo;
            }

            await DbContext.Set<MarketInformation>().AddAsync(marketInfo, cancellationToken);
            await DbContext.Entry(marketInfo).ReloadAsync(cancellationToken);
            await SaveChangesAsync();
            return marketInfo;
        }

        public async Task<IEnumerable<LandRate>> GetLandRates(CancellationToken cancellationToken = new CancellationToken())
        {
            return await DbContext.Set<LandRate>()
                .AsNoTracking()
                .Include(m => m.Country)
                .Include(m => m.Location)
                .Include(m => m.Locality)
                .Include(m => m.District)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<LandRate>> AddLandRates(IEnumerable<LandRate> landRates, CancellationToken cancellationToken)
        {
            if (landRates != null)
            {
                await DbContext.Set<LandRate>()
                    .AddRangeAsync(landRates, cancellationToken);
                await SaveChangesAsync();
                await DbContext.Entry(landRates).ReloadAsync(cancellationToken);
                return landRates;
            }

            return null;
        }

        public async Task<IEnumerable<BuildingCost>> AddBuildingCosts(IEnumerable<BuildingCost> buildingCosts, CancellationToken cancellationToken)
        {
            if (buildingCosts != null)
            {
                await DbContext.Set<BuildingCost>()
                    .AddRangeAsync(buildingCosts, cancellationToken);
                await SaveChangesAsync();
                return buildingCosts;
            }

            return null;
        }

        public async Task<IEnumerable<BuildingCost>> GetBuildingCosts(CancellationToken cancellationToken)
        {
            return await DbContext.Set<BuildingCost>()
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Comparable>> PropertySearch(PropertySearchType searchType, int locationId, string plotNo, decimal plotSize, CancellationToken cancellationToken)
        {
            // fetch records that match location#
            var allComparablesInLocation = DbContext.Set<Comparable>()
                .Include(c => c.BandClass)
                .Include(c => c.Location)
                .Include(c => c.Locality);

            if (searchType == PropertySearchType.Corporate)
            {
                return await allComparablesInLocation
                    .Where(c => c.LocationId == locationId
                                && c.PlotNo.Contains(plotNo)
                                && c.DataState == DataState.Verified)
                    .OrderBy(c => c.PlotNo)
                    .ToListAsync(cancellationToken);
            }

            // VALUER SEARCH
            allComparablesInLocation.Where(c => c.LocationId == locationId
                                                && c.DataState == DataState.Verified);
            // from result find most recent matching by plotNo as template record
            var baseComparable = allComparablesInLocation
                .Where(c => c.LocationId == locationId
                            && c.DataState == DataState.Verified)
                .OrderByDescending(c => c.DateOfSale).FirstOrDefault(c => c.PlotNo.StartsWith(plotNo));
            // if no base comparable matched
            if (baseComparable == null)
                baseComparable = allComparablesInLocation
                    .Where(c => c.LocationId == locationId
                                && c.DataState == DataState.Verified)
                    .OrderByDescending(c => c.DateOfSale).FirstOrDefault();
            // filter results on matching band
            return await allComparablesInLocation
                .Where(c =>
                    c.DataState == DataState.Verified
                    && c.LocationId == baseComparable.LocationId
                    && (!c.LocalityId.HasValue || c.LocalityId == baseComparable.LocalityId)
                    && c.BandClass == baseComparable.BandClass
                    && c.LandUse == baseComparable.LandUse)
                        .OrderByDescending(c => c.DateOfSale)
                .OrderByDescending(c => c.DateOfSale)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<BuildingMaterialCost>> AddBuildingMaterialCosts(IEnumerable<BuildingMaterialCost> buildingMaterialCosts, CancellationToken cancellationToken)
        {
            if (buildingMaterialCosts != null)
            {
                await DbContext.Set<BuildingMaterialCost>()
                    .AddRangeAsync(buildingMaterialCosts, cancellationToken);
                await SaveChangesAsync();
                return buildingMaterialCosts;
            }
            return null;
        }

        public async Task<IEnumerable<BuildingMaterialCost>> GetBuildingMaterialCosts(CancellationToken cancellationToken)
        {
            return await DbContext.Set<BuildingMaterialCost>()
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Comparable> GetPropertySearchDetails(Guid id)
        {
            var result = DbContext.Set<Comparable>()
                .Where(c => c.Id == id);
            return await result.FirstOrDefaultAsync();
        }
    }
}
