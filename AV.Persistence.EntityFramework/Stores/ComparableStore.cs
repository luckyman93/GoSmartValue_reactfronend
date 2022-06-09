using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AV.Common.Entities;
using AV.Persistence.Stores.Valuations;
using Microsoft.EntityFrameworkCore;

namespace AV.Persistence.EntityFramework.Stores
{
    public class ComparableStore: StoreBase<Comparable>, IComparableStore
    {
        public ComparableStore(ValuationsContext context): base(context)
        {
        }

        public new async Task<IEnumerable<Comparable>> Create(IEnumerable<Comparable> comparables, CancellationToken cancellationToken = default)
        {
            if (comparables == null)
                return null;
            DbSet.AddRange(comparables.ToList());
            await Context.SaveChangesAsync(cancellationToken);
            return comparables;
        }

        public async Task CreateResults(List<ComparableResult> results, CancellationToken cancellationToken)
        {
            var existingResults = Context.Set<ComparableResult>()
                .AsNoTracking()
                .Where(x => results.Select(r => r.ReferenceNumber).Contains(x.ReferenceNumber));

            // Todo: detatch object that are deep in object graph

            foreach (var comparableResult in results)
            {
                Context.Entry(comparableResult.Comparable.BandClass).State = EntityState.Detached;
                if(comparableResult.Comparable.Locality != null)
                     Context.Entry(comparableResult.Comparable.Locality).State = EntityState.Detached;
                Context.Entry(comparableResult.Comparable.Location).State = EntityState.Detached;

                foreach (var comparableResultComparable in comparableResult.Comparables)
                {
                    Context.Entry(comparableResultComparable.Comparable).State = EntityState.Detached;
                }

                if (existingResults.Any(r => comparableResult.ReferenceNumber == r.ReferenceNumber))
                {
                    Context.Entry(comparableResult).State = EntityState.Modified;
                }
                else
                {
                    Context.Entry(comparableResult).State = EntityState.Added;
                }
            }

            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}
