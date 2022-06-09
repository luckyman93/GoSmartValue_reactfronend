using AV.Common.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AV.Common.Interfaces.UnitOfWorks
{
    public interface IMarketInformationUoW
	{
		Task<IEnumerable<LandRate>> GetLandRatesAsync();
	}
}
