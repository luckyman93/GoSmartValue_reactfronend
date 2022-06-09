using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AV.Common.Entities;

namespace AV.Common.Interfaces.Repositories
{
    public interface IStreetsRepository
    {
        Street Get(int id);
        IQueryable<Street> GetAll();
        IQueryable<Street> Find(Expression<Func<Street, bool>> predicate);
        void Add(Street entity);
        void AddRange(IEnumerable<Street> entities);
        void Remove(Street entity);
        void RemoveRange(IEnumerable<Street> entities);
    }
}