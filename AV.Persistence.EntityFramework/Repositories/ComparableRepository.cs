using AV.Common.Entities;
using AV.Common.Interfaces.Repositories;
using AV.Contracts.Enums;
using AV.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AV.Persistence.EntityFramework.Repositories
{
    public class ComparableRepository : Repository<Comparable>, IComparableRepository
    {
        private readonly int _numberOfRecordForComparable = 3;

        public ComparableRepository(ValuationsContext context) : base(context)
        {
        }

        public IEnumerable<Comparable> GetAllComparables()
        {
            return DbContext.Set<Comparable>()
                .Include(b => b.BandClass)
                .Include(b => b.Locality)
                .ToList();
        }

        public ComparableBandSize GetComparableBandClass(decimal plotSize)
        {
            return DbContext.Set<ComparableBandSize>()
                       .OrderBy(c => c.LowerBandLimit)
                       .FirstOrDefault(cb =>
                           //size in band
                           (plotSize >= cb.LowerBandLimit && plotSize < cb.UpperBandLimit)
                       ) ??
                   DbContext.Set<ComparableBandSize>()
                       .OrderBy(c => c.LowerBandLimit)
                       .First();
        }

        public async Task<IEnumerable<Comparable>> FetchAll(Func<Comparable, bool> predicate)
        {
            return await Task.Run(() => DbContext.Set<Comparable>()
                .Where(predicate));
        }

        public async Task<string> UpdateComparablePaymentStatus(Guid comparableId)
        {
     
            try
            {
                var comparable = await GetComparable(comparableId);
                comparable.PaymentStatus = PaymentStatus.Paid;
                
                await SaveChangesAsync(CancellationToken.None);

                return "saved";
            }
            catch
            {
                return "failed to change comparable status";
            }

        }
        public async Task<ComparableResult> PerformComparable(Comparable comparableRequest)
        {
            comparableRequest.DataState = DataState.Raw;
            comparableRequest.PlotSize = ConvertToMetreSquared(comparableRequest.PlotSize, comparableRequest.Metric);
            comparableRequest.BandClass = GetComparableBandClass(comparableRequest.PlotSize);

            if (comparableRequest.Id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                await AddComparable(comparableRequest);
            }
            var comparableResult = new ComparableResult
            {
                Comparable = comparableRequest,
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
                var comparablesByDescending = GetComparableMatches(comparableRequest) ?? new List<Comparable>();
                if (comparablesByDescending.Count < _numberOfRecordForComparable)
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
            AddComparableResult(comparableResult);
            return await Task.FromResult(comparableResult);
        }
        public async Task<Comparable> GetComparable(Guid id)
        {
            return await DbContext.Set<Comparable>()
                .Include(f => f.Features)
                .Include(r => r.Rooms)
                .Include(l => l.Locality)
                .Include(l => l.Location)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        private static decimal GetVacantPlotEstimate(Comparable comparableRequest, LandRate landRate, decimal estimate)
        {
            if (landRate != null)
            {
                switch ((comparableRequest.BandClass.BandName))
                {
                    case "LowIncome":
                        estimate = comparableRequest.PlotSize * landRate.LowIncome;
                        break;
                    case "MiddleIncome":
                        estimate = comparableRequest.PlotSize * landRate.MiddleIncome;
                        break;
                    case "HighIncome":
                        estimate = comparableRequest.PlotSize * landRate.HighIncome;
                        break;
                }

            }

            return estimate;
        }

        private LandRate GetLandRate(Comparable comparableRequest)
        {
            return DbContext.Set<LandRate>()
                .FirstOrDefault(l => l.LocationId == comparableRequest.LocationId &&
                (
                !comparableRequest.LocalityId.HasValue
                || l.LocalityId == comparableRequest.LocalityId)
                );
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

        private List<Comparable> GetComparableMatches(Comparable comparableRequest)
        {
            return GetAllComparables()
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
                        || !LocalityVerified(comparableRequest.Locality).Verified
                        //or locality id is 0
                        || comparableRequest.LocalityId == 0
                        //otherwise include where locality id matches existing comparable locality ids
                        || v.LocalityId == comparableRequest.LocalityId)
                )
                .OrderByDescending(v => v.DateOfSale)
                .Take(_numberOfRecordForComparable)
                .ToList();
        }

        private Common.Entities.Locality LocalityVerified(Common.Entities.Locality locality)
        {
            if (locality == null)
            {
                return new Common.Entities.Locality();
            }
            return locality;
        }

        private void GetComparableMatchFillers(Comparable comparableRequest, List<Comparable> matchedComparables, int numberOfFillersNeeded)
        {
            var comparableFillers = GetAllComparables()
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
                .Take(numberOfFillersNeeded)
                .ToList();

            matchedComparables.AddRange(comparableFillers);
        }

        public decimal ConvertToMetreSquared(decimal? plotSize, Metric metric)
        {
            if (!plotSize.HasValue) return 0;
            return plotSize.Value * Constants.GetMetricMultiplierToMeterSquared(metric);
        }

        public async Task<Guid> AddComparable(Comparable comparable)
        {
            await DbContext.Set<Comparable>().AddAsync(comparable);
            Complete();
            return comparable.Id;
        }
        public IEnumerable<Comparable> GetComparablesByAddedBy(Guid UserId)
        {
            return
                DbContext.Set<Comparable>().Where(x => x.AddedBy == UserId)
                    .Include(x => x.Location);
        }

        private void AddComparableResult(ComparableResult comparableResult)
        {
            var exists = DbContext.Set<ComparableResult>()
                .AsNoTracking()
                .Any(x => x.ReferenceNumber == comparableResult.ReferenceNumber);
            if (exists)
            {
                DbContext.Set<ComparableResult>().Update(comparableResult);
            }
            else
            {
                DbContext.Set<ComparableResult>().Add(comparableResult);
            }
            Complete();
        }

        private void Complete()
        {
            DbContext.SaveChanges();
        }
    }
}
