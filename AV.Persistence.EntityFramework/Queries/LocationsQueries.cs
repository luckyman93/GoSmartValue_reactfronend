using System;
using System.Collections.Generic;
using System.Linq;
using AV.Common.Entities;
using AV.Persistence.Queries;

namespace AV.Persistence.EntityFramework.Queries
{
    public class LocationsQueries : QueriesBase<Location>, ILocationsQueries
    {
        public LocationsQueries(ValuationsContext context) : base(context)
        {
        }

        public IEnumerable<Location> FetchAllWhere(Func<Location, bool> predicate)
        {
            return _dbSet.Where(predicate);
        }
    }
}