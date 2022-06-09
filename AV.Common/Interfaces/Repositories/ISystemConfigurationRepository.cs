using System.Threading.Tasks;
using AV.Common.Entities;

namespace AV.Common.Interfaces.Repositories
{
    public interface ISystemConfigurationRepository: IRepository<SystemConfiguration>
    {
        Task<SystemConfiguration> GetInflationRate();
    }
}
