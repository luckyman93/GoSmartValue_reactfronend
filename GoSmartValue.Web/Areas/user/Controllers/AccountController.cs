using System;
using System.Threading.Tasks;
using AutoMapper;
using AV.Common.Entities;
using AV.Contracts;
using AV.Contracts.Models;
using AV.Contracts.Models.Users;
using GoSmartValue.Web.Controllers;
using GoSmartValue.Web.Services;
using GoSmartValue.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User = AV.Common.Entities.User;

namespace GoSmartValue.Web.Areas.user.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Area("user")]
    [Authorize(Policy = Constants.AccessPolicies.All)]
    public class AccountController : SecureController
    {
        private readonly IUserManagerService _userManagerService;
        private readonly IMapper _mapper;

        public AccountController(
            UserManager<User> userManager, 
            RoleManager<Role> roleManager, 
            IUserManagerService userManagerService,
            IUserManagerService usersService,
            IMapper mapper
        ): base(userManager, roleManager, usersService, mapper)
        {
            _userManagerService = userManagerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManagerService.GetUserDetails(Guid.Parse(UserManager.GetUserId(User)));
            return base.View(_mapper.Map<AV.Contracts.Models.Users.UserModel>(user));
        }

        [HttpGet]
        public IActionResult OrganisationDetail()
        {
            var organisation = _userManagerService.GetOrganisation(CurrentUser.Id);
            ViewBag.userId = CurrentUser.Id;
            if (organisation!= null)
                return View(organisation);
            return RedirectToAction("OrganisationCreate");
        }

        [HttpPost]
        public IActionResult OrganisationEdit(OrganisationViewModel organisation)
        {
            if (ModelState.IsValid)
            {
                _userManagerService.EditOrganisation(organisation);
            }

            return OrganisationDetail();
        }

        [HttpGet]
        public IActionResult OrganisationCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult OrganisationCreate(OrganisationViewModel organisation)
        {
            if (ModelState.IsValid)
            {
                //Can only create Organisation against own user account
                organisation.PrimaryUserId = CurrentUser.Id;
                _userManagerService.CreateOrganisation(organisation);
            }
            return RedirectToAction("OrganisationDetail");
        }
    }
}