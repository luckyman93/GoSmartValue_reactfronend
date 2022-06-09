using System;
using System.Collections.Generic;
using System.Linq;
using AV.Common.Entities;
using AV.Common.Interfaces.Repositories;
using AV.Common.Interfaces.UnitOfWorks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AV.Persistence.EntityFramework.UnitOfWorks
{
    public class OrganisationUnitOfWork : UnitOfWork, IOrganisationUnitOfWork
    {
        private readonly IOrganisationRepository _organisationRepository;

        public OrganisationUnitOfWork(IdentityDbContext<User, Role, Guid> dbContext, IOrganisationRepository organisationRepository)
        :base(dbContext)
        {
            _organisationRepository = organisationRepository;
        }

        public IList<Organisation> GetAll(bool activeOnly = false)
        {
            return _organisationRepository.GetAll().ToList();
        }

        public Organisation Get(Guid organisationId)
        {
            return _organisationRepository.Find(o => o.Id == organisationId)
                    .Include(o => o.UserAccounts)
                    .Include(o => o.PrimaryUser)
                    .SingleOrDefault();
        }

        public Organisation GetByUserId(Guid userId)
        {
            return _organisationRepository.Find(o => o.PrimaryUserId == userId
                                                     || o.UserAccounts.Count(us => us.Active && us.UserId == userId) > 0)
                    .Include(o => o.UserAccounts)
                    .Include(o => o.PrimaryUser)
                    .SingleOrDefault();
        }

        public void SaveOrganisation(Organisation organisation)
        {
            _organisationRepository.AddOrUpdate(organisation);
            Complete();
        }
    }
}