using AV.Common.Entities;

namespace AV.Common.Interfaces.Repositories
{
    public interface IOrganisationRepository : IRepository<Organisation>
    {
        void AddOrUpdate(Organisation organisation);
    }
}