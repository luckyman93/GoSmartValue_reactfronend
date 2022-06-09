using AV.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AV.Persistence.EntityFramework.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected ValuationsContext DbContext;

        public Repository(ValuationsContext context)
        {
            DbContext = context;
        }

        public T Get(Guid id)
        {
            return DbContext.Set<T>().Find(id);
        }

        public T Get(int id)
        {
            return DbContext.Set<T>().Find(id);
        }

        public IQueryable<T> GetAll()
        {
            return DbContext.Set<T>();
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return DbContext.Set<T>()
                .Where(predicate);
        }

        public virtual void Add(T entity)
        {
            DbContext.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            DbContext.Set<T>().AddRange(entities);
        }

        public void Update(T entity)
        {
            DbContext.Set<T>().Update(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            DbContext.Set<T>().UpdateRange(entities);
        }

        public void Remove(T entity)
        {
            DbContext.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            DbContext.Set<T>().RemoveRange(entities);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await DbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task SaveChangesAsync()
        {
            await DbContext.SaveChangesAsync();
        }
    }
}