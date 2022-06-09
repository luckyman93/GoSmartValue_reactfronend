using AV.Common.Entities;
using GoSmartValue.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GoSmartValue.Web.Pages.Shared.Components
{
    public class SideNavMenu : ViewComponent
    {
        private readonly UserManagerService _accountService;
        private readonly UserManager<User> _userManager;

        public SideNavMenu(UserManagerService accountService, UserManager<User> userManager)
        {
            _accountService = accountService;
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
