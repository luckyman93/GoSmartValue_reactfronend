using AV.Common.Entities;
using AV.Common.Interfaces.Repositories;
using AV.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AV.Persistence.EntityFramework.Repositories
{
    public class SystemConfigurationRepository : Repository<SystemConfiguration>, ISystemConfigurationRepository
    {
        public SystemConfigurationRepository(ValuationsContext context) : base(context)
        {
        }

        public async Task<SystemConfiguration> GetInflationRate()
        {
            return await DbContext.Set<SystemConfiguration>()
                .FirstOrDefaultAsync(sc => sc.ItemName == Constants.SystemConfigurations.InflationRate);
        }
    }
}
