using AV.Common.Entities;
using AV.Contracts.Enums;
using AV.Persistence.Queries;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AV.Persistence.EntityFramework.Queries
{
    public class ComparablesQueries : QueriesBase<Comparable>, IComparablesQueries
    {
        public ComparablesQueries(ValuationsContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Comparable>> FetchAllVerified(CancellationToken token)
        {
            return await _context.Set<Comparable>()
                .OrderByDescending(c => c.AddedOn)
                .Where(c => c.DataState == DataState.Verified)
                .Include(c => c.Locality)
                .Include(c => c.Location)
                .Include(c => c.BandClass)
                .Take(2000)
                .ToListAsync(token);
        }

        public async Task<IEnumerable<ComparableBandSize>> GetAllComparableBandClass()
        {
            return await _context.Set<ComparableBandSize>()
                .OrderBy(c => c.LowerBandLimit)
                .ToListAsync();
        }

        public async Task<IEnumerable<LandRate>> GetAllLandRates()
        {
            return await _context.Set<LandRate>()
                .OrderBy(c => c.LocationId)
                .ToListAsync();
        }
    }
}
