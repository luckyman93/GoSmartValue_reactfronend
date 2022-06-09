using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AV.Persistence.Stores
{
    public interface IStore<T> where T: class
    {
        Task<T> Create(T entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> Create(IEnumerable<T> entity, CancellationToken cancellationToken = default);
        Task<T> Get(Guid id, CancellationToken cancellationToken = default);
        Task<T> Update(T entity, CancellationToken cancellationToken = default);
        Task Delete(T entity, CancellationToken cancellationToken = default);
    }
}
