using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AV.Persistence.Exceptions;
using AV.Persistence.Stores;
using Microsoft.EntityFrameworkCore;

namespace AV.Persistence.EntityFramework.Stores
{
    public class StoreBase<T> : IStore<T> where T: class
    {
        protected readonly ValuationsContext Context;
        protected readonly DbSet<T> DbSet;

        public StoreBase(ValuationsContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public virtual async Task<T> Create(T entity, CancellationToken cancellationToken = default)
        {
            DbSet.Add(entity);
            await Context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<IEnumerable<T>> Create(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            var newEntities = entities.ToList();
            DbSet.AddRange(newEntities);
            await Context.SaveChangesAsync(cancellationToken);
            return newEntities;
        }

        public virtual async Task<T> Get(Guid id, CancellationToken cancellationToken = default)
        {
            await SetTransactionIsolationLevel(false, cancellationToken);
            var entity = await DbSet.FindAsync(id);
            return EntityNotFoundHandler.Handle(id, entity);
        }

        public async Task<T> Update(T entity, CancellationToken cancellationToken = default)
        {
            DbSet.Update(entity);
            await Context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task Delete(T entity, CancellationToken cancellationToken = default)
        {
            DbSet.Remove(entity);
            await Context.SaveChangesAsync(cancellationToken);
        }

        protected async Task SetTransactionIsolationLevel(bool readCommitted, CancellationToken cancellationToken)
        {
            if (Context.Database.CanConnect())
            {
                var command =  $"SET TRANSACTION ISOLATION LEVEL READ " + (readCommitted ? "COMMITTED" : "UNCOMMITTED");
                await Context.Database.ExecuteSqlRawAsync(command, cancellationToken);
            }
        }
    }
}