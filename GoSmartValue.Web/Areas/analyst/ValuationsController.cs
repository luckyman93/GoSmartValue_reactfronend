using AutoMapper;
using AV.Common.Entities;
using AV.Contracts.Enums;
using AV.Contracts.Models;
using AV.Contracts.Models.Valuation;
using GoSmartValue.Web.Controllers;
using GoSmartValue.Web.Services;
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
    [Route("valuation/")]
    [Route("[area]/valuation/")]
    public class Valuations : SecureController
    {
        private readonly IValuationsService _valuationsService;

        public Valuations(UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IValuationsService valuationsService,
            IUserManagerService usersService,
            IMapper mapper
        )
            : base(userManager, roleManager, usersService, mapper)
        {
            _valuationsService = valuationsService;
        }

        [HttpGet]
        [Route("list/{filterBy?}")]
        public IActionResult Index([FromRoute] string filterBy = "")
        {
            Enum.TryParse(filterBy, out DataState dataState);
            var valuations = _valuationsService
               .GetAllValuations(string.IsNullOrEmpty(filterBy) ? null : dataState);
            return View(valuations);
        }

        [HttpGet]
        [Route("create")]
        public IActionResult PropertyDetailCreate()
        {
            return View();
        }

        [HttpPost]
        [Route("create")]
        public IActionResult PropertyDetailCreate(ComparableViewModel comparable)
        {
            _valuationsService.AddPropertyRecord(comparable);
            return RedirectToAction("PropertyDetailCreate");
        }

        [HttpGet]
        [Route("edit")]
        public IActionResult PropertyDetailEdit(Guid id)
        {
            var valuation = _valuationsService.GetValuation(id);
            return View(valuation);
        }

        [HttpPost]
        [Route("edit")]
        public IActionResult PropertyDetailEdit(ComparableViewModel comparableViewModel)
        {
            comparableViewModel.AddedBy = CurrentUser.Id;
            comparableViewModel.LastUpdatedBy = CurrentUser.Id;
            comparableViewModel.LastUpdatedOn = DateTimeOffset.UtcNow;
            _valuationsService.SavePropertyRecord(comparableViewModel);

            return RedirectToAction("Index", new { filterBy = DataState.Pending });
        }

        [HttpGet]

        [HttpGet]
        [Route("view")]
        public IActionResult PropertyDetailView(Guid id)
        {
            var valuation = _valuationsService.GetValuation(id);
            return View(valuation);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _valuationsService.DeleteComparable(id);
            return Ok();
        }
    }
}