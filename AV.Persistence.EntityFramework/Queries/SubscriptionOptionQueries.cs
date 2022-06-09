using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AV.Common.Entities;
using AV.Persistence.Queries;
using Microsoft.EntityFrameworkCore;

namespace AV.Persistence.EntityFramework.Queries
{
    public class SubscriptionOptionQueries : QueriesBase<SubscriptionOption>, ISubscriptionOptionQueries
    {
        public SubscriptionOptionQueries(ValuationsContext context) : base(context)
        {
        }

        public IEnumerable<SubscriptionOption> FetchAllWhere(Func<SubscriptionOption, bool> predicate)
        {
            return _dbSet.AsNoTracking()
                .Include(p => p.Package)
                .ThenInclude(p => p.Products)
                .Where(predicate);
        }

        public override async Task<IEnumerable<SubscriptionOption>> FetchAll(CancellationToken cancellationToken = default)
        {
            return await _dbSet.AsNoTracking()
                .Include(p => p.Package)
                .ThenInclude(p => p.Products)
                .ToListAsync(cancellationToken);
        }
    }
    }
