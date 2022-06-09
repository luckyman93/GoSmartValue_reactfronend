using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AV.Common.Entities;
using AV.Common.Interfaces.Repositories;
using AV.Common.Interfaces.UnitOfWorks;
using AV.Contracts.Enums;
using AV.Contracts.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Country = AV.Common.Entities.Country;
using Locality = AV.Common.Entities.Locality;
using Location = AV.Common.Entities.Location;
using Street = AV.Common.Entities.Street;
using User = AV.Common.Entities.User;

namespace AV.Persistence.EntityFramework.UnitOfWorks
{
    public class LocationUnitOfWork : UnitOfWork, ILocationUnitOfWork
    {
        private readonly int _numberOfRecordForComparable;
        private IdentityDbContext<User, Role, Guid> DbContext { get; set; }
        public ILocationsRepository LocationRepository { get; set; }
        public ILocalAreaRepository LocalAreaRepository { get; set; }
        private IStreetsRepository StreetsRepository { get; set; }
        private IComparableRepository ComparableRepository { get; set; }

        public LocationUnitOfWork(IdentityDbContext<User, Role, Guid> dbContext
            , ILocationsRepository locationsRepository
            , IStreetsRepository streetsRepository
            , IComparableRepository comparableRepository
            , ILocalAreaRepository localAreaRepository)
            : base(dbContext)
        {
            DbContext = dbContext;
            LocationRepository = locationsRepository;
            LocalAreaRepository = localAreaRepository;
            StreetsRepository = streetsRepository;
            ComparableRepository = comparableRepository;

            var numberOfRecordForComparable = "";
            _numberOfRecordForComparable = !string.IsNullOrEmpty(numberOfRecordForComparable)
                ? int.Parse(numberOfRecordForComparable)
                : 3;
        }

        public ComparableResult PerformComparable(Comparable comparableRequest)
        {
            //must create new PropertyDetails records in DataState Raw pending verification process
            comparableRequest.DataState = DataState.Raw;
            //Save Valuation Request
            AddComparable(comparableRequest);
            // Perform Search for comparables
            var comparableResult = PerformComparables(comparableRequest);
            Complete();
            return comparableResult;
        }

        public static decimal ConvertToMetreSquared(decimal? plotSize, Metric metric)
        {
            if (!plotSize.HasValue) return 0;
            return plotSize.Value * Constants.GetMetricMultiplierToMeterSquared(metric);
        }

        private ComparableBandSize GetComparableBandClass(decimal plotSize)
        {
            return DbContext.Set<ComparableBandSize>()
                .OrderBy(c => c.LowerBandLimit)
                .FirstOrDefault(cb =>
                    //size in band
                    plotSize >= cb.LowerBandLimit && plotSize < cb.UpperBandLimit
                );
        }

        private ComparableResult PerformComparables(Comparable comparableRequest)
        {
            var comparablesByDescending = GetAllComparables()
                .Where(v =>
                    //Verified Records
                    v.DataState == DataState.Verified &&
                    //Add within the same band
                    v.BandClass?.BandName == comparableRequest.BandClass.BandName &&
                    //And sale price is greater than 0
                    v.SalePrice > 0 &&
                    //Same City/Town
                    v.LocationId == comparableRequest.LocationId &&
                    //same locality or Locality is not set
                    (!v.LocalityId.HasValue || v.LocalityId == comparableRequest.LocalityId)
                )
                .OrderByDescending(v => v.DateOfSale)
                .Take(_numberOfRecordForComparable)
                .ToList();

            //Average Sale Price
            decimal avgSalePrice = 0;
            if (comparablesByDescending.Any())
                avgSalePrice = comparablesByDescending.Average(c => c.SalePrice ?? 0);

            comparableRequest.SalePrice = avgSalePrice;
            var comparableResult = new ComparableResult
            {
                EstimatedValue = (double)avgSalePrice,
                ComparableId = comparableRequest.Id,
                ReferenceNumber = Guid.NewGuid(),
                EstimatedOn = DateTimeOffset.UtcNow
            };

            comparablesByDescending.ForEach(c =>
            {
                comparableResult.Comparables.Append(
                    new ComparableResultComparable
                    {
                        Comparable = c
                    });
            });

            AddComparableResult(comparableResult);
            return comparableResult;
        }

        private void AddComparableResult(ComparableResult comparableResult)
        {
            DbContext.Set<ComparableResult>().Add(comparableResult);
        }

        public IEnumerable<Comparable> GetAllComparables()
        {
            return DbContext.Set<Comparable>()
                .Include(b => b.BandClass)
                .Include(b => b.Locality)
                .ToList();
        }

        public void AddComparable(Comparable comparable)
        {
            comparable.PlotSize = ConvertToMetreSquared(comparable.PlotSize, comparable.Metric);
            comparable.BandClass = GetComparableBandClass(comparable.PlotSize);
            ComparableRepository.Add(comparable);
            DbContext.Set<Comparable>().Attach(comparable);
            Complete();
        }

        public IEnumerable<Location> GetAllLocations()
        {
            return DbContext.Set<Location>()
                .Include(l => l.LocalAreas);
        }

        public Location GetLocation(int locationId)
        {
            return LocationRepository.Get(locationId);
        }

        public async Task<Country> GetCountry(int countryId)
        {
            return await LocationRepository.GetCountry(countryId);
        }

        public Street AddStreetName(int locationId, int localityId, string streetName)
        {
            return LocationRepository.AddStreetName(locationId, localityId, streetName);
        }

        public Locality AddLocality(int locationId, string localityName)
        {
            return LocationRepository.AddLocality(locationId, localityName);
        }

        public Location AddLocation(string locationName)
        {
            return LocationRepository.AddLocation(locationName);
        }

        public IEnumerable<Location> GetAllLocationData()
        {
            return DbContext.Set<Location>()
                .Include(l => l.LocalAreas)
                .ThenInclude(s => s.Streets);
        }

        public IEnumerable<Locality> GetLocalAreas(int? locationId)
        {
            if (locationId.HasValue)
                return LocalAreaRepository.Find(l => l.LocationId == locationId).AsNoTracking();
            return LocalAreaRepository.GetAll();
        }

        public Locality GetLocalArea(int localAreaId)
        {
            return LocalAreaRepository.Get(localAreaId);
        }

        public IEnumerable<Street> GetStreets(int localityId)
        {
            return StreetsRepository.Find(l => l.LocalityId == localityId);
        }
    }
}