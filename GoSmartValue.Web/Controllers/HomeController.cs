using AutoMapper;
using AV.Common.Entities;
using AV.Contracts.Models;
using AV.Contracts.Models.Market.Requests;
using AV.Contracts.Models.Valuation;
using GoSmartValue.Web.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User = AV.Common.Entities.User;

namespace GoSmartValue.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize]
    public class HomeController : SecureController
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private IValuationsService _valuationsService;
        private readonly IMediator _mediator;
        private readonly IUserManagerService usersService;

        public HomeController(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IValuationsService valuationsService,
            IMediator mediator,
            IUserManagerService usersService,
            IMapper mapper
            )
            : base(userManager, roleManager, usersService, mapper)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _valuationsService = valuationsService;
            _mediator = mediator;
            this.usersService = usersService;
        }
        public Task<User> GetCurrentUserAsync() => UserManager.GetUserAsync(HttpContext.User);

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated
                                      /*&& CurrentUser.Accounts.Any(acc => acc.AccountType != AccountType.Standard)*/)
            {
                var user = User.Identity.Name;
                var urlRedirect = await ResolveUserRedirectUrl();
                if (urlRedirect != Constants.LandingPageUrlDefault)
                    return Redirect(urlRedirect);
                return View(new ComparableRequestViewModel());
            }

            return Redirect("/index.html");

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegistrationSuccessful()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ComparableRequestViewModel comparableRequest)
        {
            AddHttpRequestDate(comparableRequest);
            var result = (await _valuationsService.CalculateValuation(comparableRequest)).Item1;
            return RedirectToAction(
                "ValuationResult",
                "Valuations",
                new
                {
                    Area = "user",
                    result.ReferenceNumber,
                    result.EstimatedValue,
                    result.PropertyDetailsId,
                    ValuationRequest = result.ComparableRequest
                });
        }

        private void AddHttpRequestDate(ComparableRequestViewModel comparableRequestRequest)
        {
            comparableRequestRequest.RequestDate = DateTime.UtcNow;
            comparableRequestRequest.RequestUri = Request.GetDisplayUrl();
            comparableRequestRequest.Host = Request.Host.Host;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Paypal(string product)
        {
            ViewBag.Product = product;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult UploadFiles([FromForm] IEnumerable<IFormFile> files)
        {
            return Ok(files.Count());
        }

        [HttpGet]
        public async Task<IActionResult> BuildingMaterialCosts()
        {
            var result = await _mediator.Send(new GetCurrentBuildingMaterialCostsRequest());
            return View(result);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ContactUs()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Feedback()
        {
            return View();
        }


    }
}
