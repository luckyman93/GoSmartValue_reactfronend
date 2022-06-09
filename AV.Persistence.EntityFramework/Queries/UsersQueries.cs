using System;
using System.Collections.Generic;
using System.Linq;
using AV.Common.Entities;
using AV.Persistence.Queries;
using Microsoft.EntityFrameworkCore;

namespace AV.Persistence.EntityFramework.Queries
{
    public class UsersQueries : QueriesBase<User>, IUsersQueries
    {
        public UsersQueries(ValuationsContext context) : base(context)
        {
        }

        public IEnumerable<User> FetchAllWhere(Func<User, bool> predicate)
        {
            return _dbSet
                .Include(u => u.Roles)
                .Include(u => u.Accounts)
                .ThenInclude(a => a.Company)
                .Where(predicate);
        }
    }
}