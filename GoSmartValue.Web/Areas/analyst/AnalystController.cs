using System.Threading.Tasks;
using AutoMapper;
using AV.Common.Entities;
using AV.Contracts.Enums;
using AV.Contracts.Models;
using AV.Contracts.Models.Market.Requests;
using AV.Contracts.Models.Market.Requests.Deeds;
using AV.Contracts.Models.Valuation;
using GoSmartValue.Web.Controllers;
using GoSmartValue.Web.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using User = AV.Common.Entities.User;

namespace GoSmartValue.Web.Areas.analyst
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize(Policy = Constants.AccessPolicies.InternalStaff)]
    [Area("analyst")]
    public class AnalystController : SecureController
    {
        private readonly IValuationsService _valuationsService;
        private readonly IMediator _mediator;
        private readonly IUserManagerService _userService;

        public AnalystController(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IUserManagerService userService,
            IValuationsService valuationsService,
            IMediator mediator,
            IMapper mapper
            )
            : base(userManager, roleManager, userService, mapper)
        {
            _valuationsService = valuationsService;
            _mediator = mediator;
            _userService = userService;
        }

        [HttpGet]
        [Route(Constants.WebsiteRoutes.AnalystUrls.Dashboard)]
        public IActionResult Index()
        {
            return RedirectToAction("index", "account", new { area = "user" });
        }

        [HttpGet]
        [Route(Constants.WebsiteRoutes.AnalystUrls.Locations)]
        public async Task<IActionResult> Locations()
        {
            return View(await _mediator.Send(new GetAllMarketLocationsRequest()));
        }

        [HttpGet]
        [Route(Constants.WebsiteRoutes.AnalystUrls.DeedsTransactions)]
        public async Task<IActionResult> DeedsTransactions()
        {
            return View("deeds/Transactions", await _mediator.Send(new GetDeedsTransactionsRequest(250)));
        }

        [HttpGet(Constants.WebsiteRoutes.AnalystUrls.DeedsTransactionCreate)]
        public IActionResult CreateNewDeedsTransaction()
        {
            return View("deeds/CreateNewDeedsTransaction");
        }

        [HttpPost(Constants.WebsiteRoutes.AnalystUrls.DeedsTransactionCreate)]
        public IActionResult CreateNewDeedsTransaction(ComparableViewModel comparable)
        {
            if (!ModelState.IsValid)
                return View("deeds/CreateNewDeedsTransaction", comparable);

            comparable.AddedBy = _userService.GetLoggedInUserIdAsync();
            comparable.ValuationSource = ValuationSource.DeedsOffice;
            comparable.DataState = DataState.Pending;

            _valuationsService.SavePropertyRecord(comparable);

            ModelState.Clear();
            return View("deeds/CreateNewDeedsTransaction");
        }

        [HttpPost(Constants.WebsiteRoutes.AnalystUrls.DeedsTransactionEdit)]
        public IActionResult EditDeedsTransaction(Guid id, ComparableViewModel comparable)
        {
            if (!ModelState.IsValid)
                return View("deeds/EditDeedsTransaction", comparable);

            comparable.LastUpdatedBy = _userService.GetLoggedInUserIdAsync();
            comparable.LastUpdatedOn = DateTimeOffset.UtcNow;

            _valuationsService.SavePropertyRecord(comparable);

            ModelState.Clear();
            return RedirectToAction("DeedsTransactions");
        }

        [HttpGet(Constants.WebsiteRoutes.AnalystUrls.DeedsTransactionEdit)]
        public IActionResult EditDeedsTransaction([FromRoute] Guid id)
        {
            var comparable = _valuationsService.GetValuation(id);
            return View("deeds/EditDeedsTransaction", comparable);
        }

        [HttpPost(Constants.WebsiteRoutes.AnalystUrls.DeedsTransactionDelete), ActionName("DeleteTransaction")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteDeedsTransaction(Guid id)
        {
            await _valuationsService.DeleteComparable(id);
            ModelState.Clear();
            return RedirectToAction("DeedsTransactions");
        }

        [HttpPost(Constants.WebsiteRoutes.AnalystUrls.VerifyTransaction), ActionName("VerifyTransaction")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyTransaction(Guid id)
        {
            await _valuationsService.VerifyComparable(id);
            ModelState.Clear();
            return RedirectToAction("DeedsTransactions");
        }
    }
}