using System;
using System.Collections.Generic;
using AV.Common.Entities;

namespace AV.Persistence.Queries
{
    public interface IPackageQueries : IQueries<Package>
    {
        IEnumerable<Package> FetchAllWhere(Func<Package, bool> predicate);
    }
}