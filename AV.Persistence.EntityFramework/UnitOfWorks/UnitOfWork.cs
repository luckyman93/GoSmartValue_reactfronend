using System;
using AV.Common.Entities;
using AV.Common.Interfaces.UnitOfWorks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AV.Persistence.EntityFramework.UnitOfWorks
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        protected readonly IdentityDbContext<User, Role, Guid> _dbContext;

        protected UnitOfWork(IdentityDbContext<User, Role, Guid> dbContext)
        {
            _dbContext = dbContext;
        }

        public void Dispose()
        
        {
            _dbContext.Dispose();
        }

        public void Complete()
        {
            _dbContext.SaveChanges();
        }
    }
}