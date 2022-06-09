using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AV.Common.Entities;

namespace AV.Common.Interfaces.Repositories
{
    public interface ILocalAreaRepository
    {
        Locality Get(int id);
        IQueryable<Locality> GetAll();
        IQueryable<Locality> Find(Expression<Func<Locality, bool>> predicate);
        void Add(Locality entity);
        void AddRange(IEnumerable<Locality> entities);
        void Remove(Locality entity);
        void RemoveRange(IEnumerable<Locality> entities);
    }
}