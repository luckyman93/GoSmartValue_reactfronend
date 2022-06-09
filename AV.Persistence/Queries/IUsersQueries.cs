using System;
using System.Collections.Generic;
using AV.Common.Entities;

namespace AV.Persistence.Queries
{
    public interface IUsersQueries : IQueries<User>
    {
        IEnumerable<User> FetchAllWhere(Func<User, bool> predicate);
    }
}