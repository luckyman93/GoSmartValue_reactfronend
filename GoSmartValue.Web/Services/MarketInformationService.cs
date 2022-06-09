using AutoMapper;
using AV.Common.Interfaces.UnitOfWorks;
using GoSmartValue.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AV.Contracts.Models;

namespace GoSmartValue.Web.Services
{
	public class MarketInformationService : IMarketInformationService
	{
		private readonly IMarketInformationUoW _iMarketInformationUoW;
        private readonly IMapper _mapper;

        public MarketInformationService(IMarketInformationUoW iMarketInformationUoW, IMapper mapper)
        {
            _iMarketInformationUoW = iMarketInformationUoW;
            _mapper = mapper;
        }

		public async Task<ICollection<LandRateViewModel>> GetLandRates()
		{
			var landRates = await _iMarketInformationUoW.GetLandRatesAsync();
			ICollection<LandRateViewModel> mappedLandRates = new List<LandRateViewModel>();
			foreach (var landrate in landRates)
			{
				var mappedLandRate = _mapper.Map<LandRateViewModel>(landrate);
                mappedLandRates.Add(mappedLandRate);
			}
			return mappedLandRates;

		}
	}
}
