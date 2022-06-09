using AutoMapper;
using AV.Common.Entities;
using AV.Contracts.Models;
using AV.Contracts.Models.Users;
using GoSmartValue.Web.Controllers;
using GoSmartValue.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using User = AV.Common.Entities.User;

namespace GoSmartValue.Web.Areas.admin.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize(Policy = Constants.AccessPolicies.InternalStaff)]
    [Area("admin")]
    [Route("admin/users/")]
    public class UserManagerController : SecureController
    {
        private readonly ILogger<UserManagerController> _logger;
        private readonly IUserManagerService _userManagerService;

        public UserManagerController(
                ILogger<UserManagerController> logger,
                IUserManagerService userManagerService,
                UserManager<User> userManager,
                RoleManager<Role> roleManager,
                IUserManagerService usersService,
                IMapper mapper
        )
        : base(userManager, roleManager, usersService, mapper)
        {
            _logger = logger;
            _userManagerService = userManagerService;
        }

        [HttpGet("Index")]
        public ActionResult Index()
        {
            var users = _userManagerService.GetAllUsers();
            return View(users);
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details([FromRoute] Guid id)
        {
            var user = await _userManagerService.GetUserDetails(id);
            return View(user);
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit([FromRoute] Guid id)
        {
            var user = await _userManagerService.GetUserDetails(id);
            return View(user);
        }

        // POST: Admin/Edit/5
        [HttpPost("[area]/[controller]/edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        [HttpGet("Employees")]
        public IActionResult Employees()
        {
            var users = _userManagerService.GetAllUsers()
                .Where(u => u.Roles.Any(r => r.RoleName == "analyst"
                                             || r.RoleName == "admin"
                                             || r.RoleName == "manager")).ToList();
            ViewBag.Title = "All Employees";
            return View("Index", users);
        }

        [HttpGet("Accounts")]
        public IActionResult Accounts()
        {
            var accounts = _userManagerService.GetAllAccounts();
            ViewBag.Title = "All User Accounts";
            return View("Accounts", accounts);
        }

        [Route("{userId}/claims")]
        [HttpGet]
        public IActionResult ManageUserClaims(Guid userId)
        {
            var user = _userManagerService.GetUserDetails(userId);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{userId}/claims")]
        public async Task<IActionResult> ManageUserClaims(Guid userId, UserClaimsViewModel userClaim)
        {
            var user = await UserManager.FindByIdAsync(userId.ToString());
            await UserManager.AddToRoleAsync(user, userClaim.ClaimName);

            var userViewModel = _userManagerService.GetUserDetails(userId);
            return View(userViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        
        // GET: Admin/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}