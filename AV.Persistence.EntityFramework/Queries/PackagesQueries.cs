using AV.Common.Entities;
using AV.Persistence.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AV.Persistence.EntityFramework.Queries
{
    public class PackagesQueries : QueriesBase<Package>, IPackageQueries
    {
        public PackagesQueries(ValuationsContext context) : base(context)
        {
        }

        public IEnumerable<Package> FetchAllWhere(Func<Package, bool> predicate)
        {
            return _dbSet.AsNoTracking()
                .Include(p => p.Features)
                .Where(predicate);
        }

        public override async Task<IEnumerable<Package>> FetchAll(CancellationToken cancellationToken = default)
        {
            return await _dbSet.AsNoTracking()
                .Include(p => p.Features)
                .ToListAsync(cancellationToken);
        }
    }
}