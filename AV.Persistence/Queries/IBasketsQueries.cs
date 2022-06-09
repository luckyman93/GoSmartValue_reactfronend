using AV.Common.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AV.Persistence.Queries;

public interface IBasketsQueries : IQueries<Basket>
{
    Task<Basket> GetOrCreateCurrentUserBasketAsync(Guid userId, CancellationToken cancellationToken);
    Basket GetOrCreateCurrentUserBasket(Guid userId, CancellationToken cancellationToken);
    Task<Basket> GetCurrentUserBasketAsync(Guid userId, CancellationToken cancellationToken);
    Task<IEnumerable<Basket>> GetAllUsersPaidBaskets(Guid userId);
}