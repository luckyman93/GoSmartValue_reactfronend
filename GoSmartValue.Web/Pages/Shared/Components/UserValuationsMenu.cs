using AV.Common.Entities;
using GoSmartValue.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GoSmartValue.Web.Pages.Shared.Components
{
    public class UserValuationsMenu : ViewComponent
    {
        private readonly UserManagerService _accountService;
        private readonly UserManager<User> _userManager;
        private readonly IValuationsService _valuationsService;

        public UserValuationsMenu(
            UserManagerService accountService,
            UserManager<User> userManager, 
            IValuationsService valuationsService)
        {
            _accountService = accountService;
            _userManager = userManager;
            _valuationsService = valuationsService;
        }

        public IViewComponentResult Invoke()
        {
           return View();
        }
    }
}
