using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AV.Persistence.Queries
{
    public interface IQueries<T> where T : class
    {
        Task<IEnumerable<T>> FetchAll(CancellationToken cancellationToken = default);
        T Find(Func<T, bool> predicate);
        IQueryable<T> FindAll();
        DbSet<T> EntityDbSet { get; }
        Task<IEnumerable<T>> FetchAll(Func<T, bool> predicate);
    }
}