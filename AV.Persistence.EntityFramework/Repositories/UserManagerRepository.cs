using AV.Common.Entities;
using AV.Common.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AV.Persistence.EntityFramework.Repositories
{
    public class UserManagerRepository : Repository<User>, IUserManagerRepository
    {
        public UserManagerRepository(ValuationsContext context) : base(context)
        {
        }

        public async Task<User> GetAsync(Guid userId)
        {
            return await DbContext.Set<User>()
                .Where(u => u.Id == userId)
                .Include(u => u.Accounts)
                .ThenInclude(ac => ac.BankAccounts)
                .FirstOrDefaultAsync();
        }
    }
}