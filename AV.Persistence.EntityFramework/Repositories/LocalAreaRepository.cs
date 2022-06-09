using AV.Common.Entities;
using AV.Common.Interfaces.Repositories;

namespace AV.Persistence.EntityFramework.Repositories
{
    public class LocalAreaRepository : Repository<Locality>, ILocalAreaRepository
    {
        public LocalAreaRepository(ValuationsContext context) : base(context)
        {

        }
    }
}