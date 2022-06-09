using AutoMapper;
using AV.Common;
using AV.Common.Entities;
using AV.Contracts;
using AV.Contracts.Models;
using GoSmartValue.Web.Controllers;
using GoSmartValue.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User = AV.Common.Entities.User;

namespace GoSmartValue.Web.Areas.analyst
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Area("analyst")]
    [Authorize(Policy = Constants.AccessPolicies.InternalStaff)]
    public class UsersController : SecureController
    {
        public UsersController(UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IUserManagerService usersService,
            IMapper mapper
        )
            : base(userManager, roleManager, usersService, mapper)
        {
        }

        [HttpGet]
        [Route(Constants.WebsiteRoutes.AnalystUrls.ManageUser)]
        public IActionResult Index()
        {
            return View();
        }
    }
}