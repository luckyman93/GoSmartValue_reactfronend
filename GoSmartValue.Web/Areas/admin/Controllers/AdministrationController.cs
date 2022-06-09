using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using AutoMapper;
using AV.Common.Entities;
using AV.Contracts;
using AV.Contracts.Models;
using AV.Contracts.Models.Users;
using GoSmartValue.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User = AV.Common.Entities.User;

namespace GoSmartValue.Web.Areas.admin.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize(Policy = Constants.AccessPolicies.Admin)]
    [Route("admin/")]
    [Area("admin")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public AdministrationController(RoleManager<Role> roleManager, UserManager<User> userManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("listroles")]
        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles.Select(role => new RoleViewModel {Id = role.Id, RoleName = role.Name});
            return View(roles);
        }

        [HttpGet]
        [Route("createrole")]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        [Route("createrole")]
        public async Task<IActionResult> CreateRole([FromForm] RoleViewModel roleModel)
        {
            if (!ModelState.IsValid)
                return View();
            var role = new Role
            {
                Name = roleModel.RoleName
            };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("ListRoles", "Administration");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return View();
        }

        [HttpGet]
        [Route("editerole/{id}")]
        public async Task<IActionResult> EditRole(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
            {
                ViewBag.ErrorMessage = $"UserRole with id {id} cannot be found.";
                return BadRequest("NotFound");
            }

            var roleViewModel = new EditRoleViewModel { RoleName = role.Name, Id = role.Id, Users = new List<AV.Contracts.Models.Users.UserModel>()};

            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    roleViewModel.Users.Add(_mapper.Map<AV.Contracts.Models.Users.UserModel>(user));
                }
            }

            return View(roleViewModel);
        }

        [HttpPost]
        [Route("editerole")]
        public async Task<IActionResult> EditRole(RoleViewModel roleModel)
        {
            if (!ModelState.IsValid)
                return View();
            var role = await _roleManager.FindByIdAsync(roleModel.Id.ToString());
            if (role == null)
            {
                ViewBag.ErrorMessage = $"UserRole with id '{roleModel.Id}' cannot be found.";
                return BadRequest("NotFound");
            }

            role.Name = roleModel.RoleName;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("ListRoles");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return View();
        }

        [HttpGet]
        [Route("editusersinrole/{roleId}")]
        public async Task<IActionResult> EditUsersInRole([FromRoute] Guid roleId)
        {
            ViewBag.roleId = roleId;

            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
            {
                ViewBag.ErrorMessage = $"UserRole with id '{roleId}' cannot be found.";
                return BadRequest("NotFound");
            }

            var users = new List<UserRoleViewModel>();
            foreach (var user in _userManager.Users)
            {
                var userVievModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                userVievModel.IsSelected = await _userManager.IsInRoleAsync(user, role.Name);

                users.Add(userVievModel);
            }

            return View(users);
        }

        [HttpPost]
        [Route("editusersinrole/{roleId}")]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> users, Guid roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
            {
                ViewBag.ErrorMessage = $"UserRole with id '{roleId}' cannot be found.";
                return BadRequest("NotFound");
            }

            for (int i = 0; i < users.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(users[i].UserId.ToString());

                IdentityResult result = null;
                if (users[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!users[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (users.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new {Id = roleId});
                }
            }

            return RedirectToAction("EditRole", new { Id = roleId });
        }

        [HttpPost("users/{userId}")]
        public async Task<IActionResult> ChangeUserPassword([FromRoute] Guid userId, [FromBody] string password)
        {
            // check password
            if (await ValidatePassword(password))
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                    throw new AuthenticationException("User not found.");

                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, password);

                return Ok("Password has been set successfully.");
            }
            throw new AuthenticationException("The password does not meet criteria.");
        }

        private async Task<bool> ValidatePassword(string password)
        {
            var passwordErrors = new List<string>();

            var validators = _userManager.PasswordValidators;

            foreach (var validator in validators)
            {
                var result = await validator.ValidateAsync(_userManager, null, password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        passwordErrors.Add(error.Description);
                    }
                }
            }

            return passwordErrors.Count == 0;
        }
    }
}