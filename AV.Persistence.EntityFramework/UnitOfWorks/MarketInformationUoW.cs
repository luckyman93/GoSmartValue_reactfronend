using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AV.Common.Entities;
using AV.Common.Interfaces;
using AV.Common.Interfaces.UnitOfWorks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AV.Persistence.EntityFramework.UnitOfWorks
{
    public class MarketInformationUoW : UnitOfWork, IMarketInformationUoW
	{
		private readonly IMarketInformationRepository _iMarketInformationRepo;

		public MarketInformationUoW(IdentityDbContext<User, Role, Guid> dbContext,
			IMarketInformationRepository iMarketInformationRepo) : base(dbContext)
		{
			_iMarketInformationRepo = iMarketInformationRepo;
		}

		public async Task<IEnumerable<LandRate>> GetLandRatesAsync()
		{
			return await _iMarketInformationRepo.GetLandRates(new CancellationToken());
		}
	}
}
