using System;
using AV.Common.Entities;
using GoSmartValue.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GoSmartValue.Web.Pages.Shared.Components
{
    public class OrganisationMenu : ViewComponent
    {
        private readonly UserManagerService _accountService;
        private readonly UserManager<User> _userManager;

        public OrganisationMenu(UserManagerService accountService, UserManager<User> userManager)
        {
            _accountService = accountService;
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var organisationViewModel =
                _accountService.GetOrganisation(Guid.Parse(_userManager.GetUserId(Request.HttpContext.User)));
            return View(organisationViewModel);
        }
    }
}
