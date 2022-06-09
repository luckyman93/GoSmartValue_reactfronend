using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using AV.Persistence.Queries;
using Microsoft.EntityFrameworkCore;

namespace AV.Persistence.EntityFramework.Queries
{
    public class QueriesBase<T> : IQueries<T> where T : class
    {
        protected readonly DbSet<T> _dbSet;
        protected readonly ValuationsContext _context;

        public QueriesBase( ValuationsContext context)
        {
            _dbSet = context.Set<T>();
            _context = context;
        }

        public virtual async Task<IEnumerable<T>> FetchAll(CancellationToken cancellationToken = default)
        {
            return await _dbSet.AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public virtual T Find(Func<T, bool> predicate)
        {
            return _dbSet.Where(predicate).FirstOrDefault();
        }

        public virtual IQueryable<T> FindAll()
        {
            return _dbSet.AsQueryable();
        }

        public DbSet<T> EntityDbSet => _dbSet;
        public virtual async Task<IEnumerable<T>> FetchAll(Func<T, bool> predicate)
        {
            return _dbSet.Where(predicate).AsQueryable();
        }
    }
}