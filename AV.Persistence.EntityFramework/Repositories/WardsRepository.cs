using AV.Common.Entities;
using AV.Common.Interfaces.Repositories;

namespace AV.Persistence.EntityFramework.Repositories
{
    public class StreetsRepository : Repository<Street>, IStreetsRepository
    {
        public StreetsRepository(ValuationsContext context) : base(context)
        {

        }
    }
}