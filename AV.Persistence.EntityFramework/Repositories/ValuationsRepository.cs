using AV.Common.Entities;
using AV.Common.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AV.Persistence.EntityFramework.Repositories
{
    public class ValuationsRepository : Repository<Valuation>, IValuationsRepository
    {
        public ValuationsRepository(ValuationsContext context) : base(context)
        {
        }

        public new void Add(Valuation entity)
        {
            var exists = DbContext.Set<Valuation>()
                .AsNoTracking()
                .Any(x => x.Id == entity.Id);
            if (exists)
            {
                base.Update(entity);
            }
            else
            {
                base.Add(entity);
            }
        }
    }
}