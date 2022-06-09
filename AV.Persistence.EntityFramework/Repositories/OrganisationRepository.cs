using AV.Common.Entities;
using AV.Common.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;

namespace AV.Persistence.EntityFramework.Repositories
{
    public class OrganisationRepository : Repository<Organisation>, IOrganisationRepository
    {
        private DbContext _context;

        public OrganisationRepository(ValuationsContext context) : base(context)
        {
            _context = context;
        }

        public void AddOrUpdate(Organisation organisation)
        {
            if (organisation.Id != default(Guid))
            {
                try
                {
                    _context.Set<Organisation>().Attach(organisation);
                    _context.Entry(organisation).State = EntityState.Modified;
                }
                catch (Exception exception)
                {
                    Debug.WriteLine($"Failed to save Organisation. Id exists. ID: {organisation.Id}{Environment.NewLine}: Error Message:{exception}");
                }
                return;
            }
            this.Add(organisation);
        }
    }
}