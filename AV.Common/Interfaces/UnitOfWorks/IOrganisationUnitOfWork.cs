using AV.Common.Entities;
using System;
using System.Collections.Generic;

namespace AV.Common.Interfaces.UnitOfWorks
{
    public interface IOrganisationUnitOfWork : IUnitOfWork
    {
        IList<Organisation> GetAll(bool activeOnly = false);
        Organisation Get(Guid organisationId);
        Organisation GetByUserId(Guid userId);
        void SaveOrganisation(Organisation organisation);
    }
}