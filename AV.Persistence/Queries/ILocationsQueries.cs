using System;
using System.Collections.Generic;
using AV.Common.Entities;

namespace AV.Persistence.Queries
{
    public interface ILocationsQueries : IQueries<Location>
    {
        IEnumerable<Location> FetchAllWhere(Func<Location, bool> predicate);
    }
}