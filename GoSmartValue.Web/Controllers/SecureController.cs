using AutoMapper;
using AV.Common.Constants;
using AV.Common.Entities;
using AV.Contracts.Enums;
using AV.Contracts.Models;
using AV.Contracts.Models.Users;
using GoSmartValue.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using User = AV.Common.Entities.User;

namespace GoSmartValue.Web.Controllers
{
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class SecureController : Controller
    {
        public readonly UserManager<User> UserManager;
        public RoleManager<Role> RoleManager;
        private protected readonly IUserManagerService _userService;
        private readonly IMapper _mapper;
        private User _currentUser { get; set; }

        public SecureController(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IUserManagerService userService,
            IMapper mapper)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            _userService = userService;
            _mapper = mapper;
        }

        internal User CurrentUser
        {
            get
            {
                if (!User.Identity.IsAuthenticated || _currentUser != null)
                    return _currentUser;
                SetCurrentUser(User.Identity.Name);
                return _currentUser;
            }
            set => _currentUser = value;
        }

        internal async Task<bool> IsValuer()
        {
            return await UserManager.IsInRoleAsync(CurrentUser, UserRoles.Valuer);
        }

        internal async Task<bool> IsCorporate()
        {
            return await UserManager.IsInRoleAsync(CurrentUser, UserRoles.Corporate);
        }

        internal async Task<bool> CanTransact(SubscriptionType subTypeType)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _userService.CanTransactAsync(Guid.Parse(userId), subTypeType);
        }


        internal bool CurrentUserIsAdmin() => User.IsInRole(UserRoles.Admin);

        private async Task SetCurrentUser(string userName)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userService.GetUserDetails(Guid.Parse(userId));
            _currentUser = _mapper.Map<User>(user);
        }

        internal async Task<string> ResolveUserRedirectUrl(UserLoginViewModel userModel = null)
        {
            if (userModel == null && CurrentUser == null)
                return Constants.LandingPageUrlDefault;

            userModel ??= _mapper.Map<UserLoginViewModel>(CurrentUser);
            if (!string.IsNullOrEmpty(userModel.ReturnUrl))
            {
                return userModel.ReturnUrl;
            }

            var user = await UserManager.FindByEmailAsync(userModel.UserName);
            if (user == null)
            {
                return Constants.LandingPageUrlDefault;
            }

            var roles = await UserManager.GetRolesAsync(user);
            if (roles.Contains(UserRoles.Admin))
            {
                return Constants.LandingPageUrlAdmin;
            }

            if (roles.Contains(UserRoles.Analyst))
            {
                return Constants.LandingPageUrlAnalyst;
            }

            if (roles.Contains(UserRoles.Corporate))
            {
                return Constants.LandingPageUrlCorporate;
            }

            if (roles.Contains(UserRoles.Valuer))
            {
                return Constants.LandingPageUrlValuer;
            }

            if (roles.Contains(UserRoles.Standard))
            {
                return Constants.LandingPageUrlStandard;
            }

            return roles.Contains(UserRoles.SalesAgent) ? Constants.LandingPageUrlSalesAgent : Constants.LandingPageUrlDefault;
        }

        internal void ResetReturnUrl()
        {
            if (CurrentUser == null)
                return;
            _userService.ResetReturnUrl(CurrentUser);
        }

        internal async Task<Guid> UserAccountId()
        {
            var id = await _userService.GetActiveAccountId(CurrentUser.Id);
            return id;
        }


    }

}