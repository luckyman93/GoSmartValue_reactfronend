using AutoMapper;
using AV.Common.Entities;
using AV.Contracts.Models;
using AV.Contracts.Models.Market.Commands;
using AV.Contracts.Models.Market.Requests;
using AV.Contracts.Models.Market.ResponseModels;
using GoSmartValue.Web.Controllers;
using GoSmartValue.Web.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using User = AV.Common.Entities.User;

namespace GoSmartValue.Web.Areas.analyst
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize(Policy = Constants.AccessPolicies.InternalStaff)]
    [Area("analyst")]
    public class MarketController : SecureController
    {
        private readonly IMediator _mediator;

        public MarketController(
            IMediator mediator,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IUserManagerService usersService,
            IMapper mapper
        )
            : base(userManager, roleManager, usersService, mapper)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route(Constants.WebsiteRoutes.AnalystUrls.MarketInformation.LandRates)]
        public async Task<IActionResult> LandRates()
        {
            var result = await _mediator.Send(new GetAllCurrentLandRatesRequest());
            return View("LandRates", result);
        }

        [HttpGet]
        [Route(Constants.WebsiteRoutes.AnalystUrls.MarketInformation.LandRatesImport)]
        public IActionResult LandRatesImport()
        {
            return View("LandRatesImport");
        }

        [HttpPost]
        [Route(Constants.WebsiteRoutes.AnalystUrls.MarketInformation.LandRatesImport)]
        public async Task<IActionResult> UploadLandRates([FromForm] ImportLandRatesCommand command)
        {

            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(command);
                return View("LandRates", result);
            }

            return View(command);
        }


        [HttpGet]
        [Route(Constants.WebsiteRoutes.AnalystUrls.MarketInformation.BuildingCosts)]
        public async Task<IActionResult> BuildingCosts()
        {
            var result = await _mediator.Send(new GetCurrentBuildingCostsRequest());
            return View("BuildingCosts", result);
            //return View();
        }
        [HttpGet]
        [Route(Constants.WebsiteRoutes.AnalystUrls.MarketInformation.BuildingCostsImport)]
        public IActionResult BuildingCostsImport()
        {
            return View("BuildingCostsImport");
        }

        [HttpPost]
        [Route(Constants.WebsiteRoutes.AnalystUrls.MarketInformation.BuildingCostsImport)]
        public async Task<IActionResult> BuildingCosts([FromForm] ImportBuildingCostsCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(command);
                return View("BuildingCosts", result);
            }

            return View(command);
        }

        [HttpGet]
        [Route(Constants.WebsiteRoutes.AnalystUrls.MarketInformation.BuildingMaterialCostsImport)]
        public IActionResult BuildingMaterialCostsImport()
        {
            return View();
        }

        [HttpPost]
        [Route(Constants.WebsiteRoutes.AnalystUrls.MarketInformation.BuildingMaterialCostsImport)]
        public async Task<IActionResult> BuildingMaterialCostsImport([FromForm] ImportBuildingMaterialCostsCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(command);
                return View("BuildingMaterialCosts", result);
            }

            return View(command);
        }

        [HttpGet]
        [Route(Constants.WebsiteRoutes.AnalystUrls.MarketInformation.BuildingMaterialCosts)]
        public async Task<IActionResult> BuildingMaterialCosts()
        {
            var result = await _mediator.Send(new GetCurrentBuildingMaterialCostsRequest());
            return View(result);
        }
    }
}