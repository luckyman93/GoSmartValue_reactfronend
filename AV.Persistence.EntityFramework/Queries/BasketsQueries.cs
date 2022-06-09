using AV.Common.Entities;
using AV.Contracts.Enums;
using AV.Persistence.Queries;
using AV.Persistence.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AV.Persistence.EntityFramework.Queries
{
    public class BasketsQueries : QueriesBase<Basket>, IBasketsQueries
    {
        private readonly IStore<Basket> _basketsStore;
        private readonly ILogger<BasketsQueries> _logger;

        public BasketsQueries(ValuationsContext context,
            IStore<Basket> basketsStore,
            ILogger<BasketsQueries> logger) : base(context)
        {
            _basketsStore = basketsStore;
            _logger = logger;
        }

        public override Basket Find(Func<Basket, bool> predicate)
        {
            return _dbSet.AsNoTracking()
                .Include(b => b.Items)
                .Include(b => b.Status)
                .Where(predicate)
                .FirstOrDefault();
        }
        public override async Task<IEnumerable<Basket>> FetchAll(Func<Basket, bool> predicate)
        {
            return _dbSet
               .Include(b => b.Items)
               .Include(b => b.Status)
               .Where(predicate);
        }

        public async Task<Basket> GetOrCreateCurrentUserBasketAsync(Guid userId, CancellationToken cancellationToken)
        {
            var basket = Find(b => b.BuyerId == userId && (Equals(b.Status, BasketStatus.Draft) || Equals(b.Status, BasketStatus.Confirmed)));

            if (basket != null)
                return basket;

            basket = new Basket(userId);
            await _basketsStore.Create(basket, cancellationToken);
            _logger.LogInformation($"New Basket created for userId#{userId}. basketId#{basket.Id}");
            return basket;
        }

        public Basket GetOrCreateCurrentUserBasket(Guid userId, CancellationToken cancellationToken)
        {
            var basket = Find(b => b.BuyerId == userId && Equals(b.Status, BasketStatus.Draft) || Equals(b.Status, BasketStatus.Confirmed));

            if (basket != null)
                return basket;

            basket = new Basket(userId);
            Task.Run(async () => await _basketsStore.Create(basket, cancellationToken));
            _logger.LogInformation($"New Basket created for userId#{userId}. basketId#{basket.Id}");

            return basket;
        }

        public async Task<Basket> GetCurrentUserBasketAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _dbSet.FirstOrDefaultAsync(b => b.BuyerId == userId && Equals(b.Status, BasketStatus.Draft) || Equals(b.Status, BasketStatus.Confirmed),
                cancellationToken);
        }

        public async Task<IEnumerable<Basket>> GetAllUsersPaidBaskets(Guid userId)
        {
            return await FetchAll(b => b.BuyerId == userId
                && Equals(b.Status, BasketStatus.Paid)
                || Equals(b.Status, BasketStatus.Completed));
        }
    }
}
