using System.Threading.Tasks;
using AutoMapper;
using AV.Common;
using AV.Common.Entities;
using AV.Contracts;
using AV.Contracts.Models;
using AV.Contracts.Models.Market.Requests;
using GoSmartValue.Web.Controllers;
using GoSmartValue.Web.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User = AV.Common.Entities.User;

namespace GoSmartValue.Web.Areas.api.Controllers
{
	[ApiController]
	public class MarketInformationController : SecureController
	{
		private readonly IMediator _mediator;

		public MarketInformationController(
			UserManager<User> userManager,
			RoleManager<Role> roleManager,
			IUserManagerService userService,
			IMediator mediator,
            IMapper mapper
            )
			: base(userManager, roleManager, userService, mapper)
		{
			_mediator = mediator;
		}

		[HttpGet]
		[Route(Constants.ApiRoutes.Market.GetLandRates)]
		public async Task<IActionResult> GetLandRates([FromQuery] GetLandRatesRequest request)
		{
			return Ok(await _mediator.Send(request));
		}

		[HttpGet]
		[Route(Constants.ApiRoutes.Market.GetAveragePlotSize)]
		public async Task<IActionResult> GetAveragePlotSize([FromQuery] GetAveragePlotSizeRequest request)
		{
			return Ok(await _mediator.Send(request));
		}

		[HttpGet]
		[Route(Constants.ApiRoutes.Market.GetAverageSellingPrice)]
		public async Task<IActionResult> GetAverageSellingPrice([FromQuery] GetAverageSellingPriceRequest request)
		{
			return Ok(await _mediator.Send(request));
		}

		[HttpGet]
		[Route(Constants.ApiRoutes.Market.GetInflationRate)]
		public async Task<IActionResult> GetInflationRate()
		{
			return Ok(await _mediator.Send(new GetInflationRateRequest()));
		}

		[HttpPost]
		[Route(Constants.ApiRoutes.Market.PostInflationRate)]
		public async Task<IActionResult> PostInflationRate()
		{
			return Ok(await _mediator.Send(new AddInflationRateRequest()));
		}

		[HttpPost]
		[Route(Constants.ApiRoutes.Market.PostMarketInformation)]
		public async Task<IActionResult> PostMarketInformation(AddMarketInformationCommand request)
		{
			return Ok(await _mediator.Send(request));
		}
	}
}