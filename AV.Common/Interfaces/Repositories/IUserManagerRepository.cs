using System;
using System.Threading.Tasks;
using AV.Common.Entities;

namespace AV.Common.Interfaces.Repositories
{
    public interface IUserManagerRepository : IRepository<User>
    {
        Task<User> GetAsync(Guid userId);
    }
}