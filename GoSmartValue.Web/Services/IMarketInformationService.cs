using GoSmartValue.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoSmartValue.Web.Services
{
	public interface IMarketInformationService
	{
		Task<ICollection<LandRateViewModel>> GetLandRates();
	}
}
