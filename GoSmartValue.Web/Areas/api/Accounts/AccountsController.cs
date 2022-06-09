using AutoMapper;
using AV.Common.Entities;
using AV.Common.Interfaces;
using AV.Contracts;
using AV.Contracts.Enums;
using AV.Contracts.Models;
using AV.Contracts.Models.Accounts.Commands;
using AV.Contracts.Models.Users;
using GoSmartValue.Web.AppStartConfigs;
using GoSmartValue.Web.Areas.api.Models;
using GoSmartValue.Web.Models;
using GoSmartValue.Web.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using UserModel = AV.Contracts.Models.Users.UserModel;

namespace GoSmartValue.Web.Areas.api.Accounts
{
    [ApiController]
    [Produces("application/json")]
    [ApiTokenAuth]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IUserManagerService _userManagerService;
        private readonly IMediator _mediator;
        private readonly IAccountsRepository _accountsRepository;
        private readonly ILogger<AccountsController> _logger;
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;


        public AccountsController(
            UserManager<User> userManager,
            IUserManagerService userManagerService,
            RoleManager<Role> roleManager,
            SignInManager<User> signInManager,
            IAccountsRepository accountsRepository,
            ILogger<AccountsController> logger,
            IMediator mediator,
            IMapper mapper)
        {
            _userManagerService = userManagerService;
            _signInManager = signInManager;
            _accountsRepository = accountsRepository;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;

            _mediator = mediator;
        }

        [HttpGet(ApiUrlConstants.Accounts.GetUserStatus)]
        public async Task<IActionResult> UserAccountStatus([FromRoute] string username)
        {
            var user = await _userManager.FindByEmailAsync(username);
            return Ok(new ApiResponse(HttpStatusCode.OK,
                data: new
                {
                    isRegistered = user != null,
                    isConfirmed = user?.EmailConfirmed ?? false
                }));
        }

        [HttpPost(ApiUrlConstants.Accounts.RegisterStandardUserPost)]
        public async Task<IActionResult> Register([FromBody] RegisterUserModel userModel)
        {
            var user = CreateUserModel(userModel);
            var userRole = AV.Common.Constants.UserRoles.Standard;
            
            if (userModel.ReacNumber != null)
            {
                userRole = AV.Common.Constants.UserRoles.Valuer;
                user.ReacNumber = userModel.ReacNumber;
                user.ReibNumber = userModel.ReibNumber;
            }
            
            user.Active = true;
            var userCreatedResult = await _userManager.CreateAsync(user);
            if (!userCreatedResult.Succeeded)
                return Conflict(new ApiResponse(HttpStatusCode.OK,
                    data: userCreatedResult));
            
            await _userManager.AddToRoleAsync(user, userRole);

            if (userRole == AV.Common.Constants.UserRoles.Standard)
            {
                _userManagerService.CreateUserAccountRecord(user, SubscriptionType.Standard);
            }
            else if(userRole == AV.Common.Constants.UserRoles.Valuer)
            {
                _userManagerService.CreateUserAccountRecord(user, SubscriptionType.Standard, isValuer:true);
            }
            else
            {
                _userManagerService.CreateUserAccountRecord(user, SubscriptionType.Standard, isCorporate:true);
            }

            await _userManager.AddPasswordAsync(user, userModel.Password);
            return Ok(new ApiResponse(HttpStatusCode.OK,
                data: new
                {
                    UserName = userModel.Email,
                    userModel.FirstName,
                    userModel.LastName,
                    userModel.PhoneNumber,
                    userModel.Email
                }));

        }


        [HttpPut(ApiUrlConstants.Accounts.PUT_EditUserProfile)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> EditUser([FromBody] UpdateUserProfileCommand updateUserProfileCommand)
        {
            if(ModelState.IsValid)
            {
                if (!this.User.Identity.IsAuthenticated)
                    return Unauthorized("User is not currently logged");

                var user = await _userManager.GetUserAsync(this.User);

                if(!string.IsNullOrEmpty(updateUserProfileCommand.FirstName))
                    user.FirstName = updateUserProfileCommand.FirstName;
                if (!string.IsNullOrEmpty(updateUserProfileCommand.LastName))
                    user.LastName = updateUserProfileCommand.LastName;
                if (!string.IsNullOrEmpty(updateUserProfileCommand.PhoneNumber))
                    user.PhoneNumber = updateUserProfileCommand.PhoneNumber;

                await _userManager.UpdateAsync(user);

                return Ok(new ApiResponse(HttpStatusCode.OK,
                    data: new GetUserDetailsModel()
                    {
                        UserId = user.Id,
                        UserName = user.UserName,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        Email = user.Email,
                        Subscription = await _mediator.Send(new GetUserSubscriptionRequest(userId: user.Id)),
                    }));
            }
            return BadRequest("Unable to update user details.");
        }

        [HttpPost(ApiUrlConstants.Accounts.AccountConfirmation)]
        [Authorize(Policy = Constants.AccessPolicies.Admin)]
        public async Task<IActionResult> AccountConfirmationAdmin([FromBody] ConfirmUserAccountCommand confirmUserAccountCommand)
        {
            var user = await _userManager.FindByEmailAsync(confirmUserAccountCommand.UserName);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with Username '{confirmUserAccountCommand.UserName}'.");
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                await _userManager.ConfirmEmailAsync(user, token);
            }

            return Created(nameof(AccountConfirmationAdmin), _mapper.Map<UserModel>(user));
        }

        [HttpGet(ApiUrlConstants.Accounts.AccountConfirmation)]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
                return BadRequest();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                // throw new ApplicationException($"Unable to load user with ID '{userId}'.");
                return BadRequest();
            }

            var result = await _userManager.ConfirmEmailAsync(user, HttpUtility.UrlDecode(code));

            if (result.Succeeded)
                return Ok();

            return BadRequest();
        }

        [HttpPost(ApiUrlConstants.Accounts.LoginPost)]
        public async Task<IActionResult> Login([FromBody] UserLoginModel loginModel)
        {
            if (await IsValidUsernameAndPassword(loginModel.UserName, loginModel.Password))
            {
                bool redirect = false;
                var user = await _userManager.FindByEmailAsync(loginModel.UserName);
                var userRoles = await _userManager.GetRolesAsync(user);
                var userToken = AuthConfigurations.GetJwtToken(user, userRoles);

                var userAccount = await _accountsRepository.GetUserAccount(user.Id);
                var portalUrl = userAccount.AccountType switch
                {
                    AccountType.Standard => "/portal",
                    AccountType.Corporate => "corporate",
                    _ => "valuer"
                };

                if (userAccount.AccountType != AccountType.Standard)
                {
                    redirect = true;
                    await _signInManager.SignInAsync(user, false);
                }

                var responseToken = new UserTokenViewModel
                {
                    Token = userToken,
                    UserName = user.UserName,
                    Redirect = redirect,
                    PortalUrl = portalUrl
                };
                return Ok(responseToken);
            }

            return BadRequest("Invalid login details.");
        }

        [HttpGet(ApiUrlConstants.Accounts.LogOutGet)]
        public async Task<IActionResult> LogOut()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return Ok("user logout successfully.");
            }
            catch (Exception exception)
            {
                _logger.LogError($"Unable to logout user.", exception);
                return BadRequest("user logout successfully.");
            }
        }

        [HttpGet(ApiUrlConstants.Accounts.LoggedInUserDetailsGet)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UserDetails()
        {
            if (!this.User.Identity.IsAuthenticated)
                return Unauthorized("User is not currently logged");

            var user = await _userManager.GetUserAsync(this.User);
            var userAccount = await _accountsRepository.GetUserAccount(user.Id);
            var portalUrl = userAccount.AccountType switch
            {
                AccountType.Standard => "/portal",
                AccountType.Corporate => "corporate",
                _ => "valuer"
            };
            return Ok(new ApiResponse(HttpStatusCode.OK,
                data: new GetUserDetailsModel()
                {
                    UserId = user.Id,
                    UserName = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    Subscription = await _mediator.Send(new GetUserSubscriptionRequest(userId: user.Id)),
                    PortalUrl = portalUrl
                }));
        }

        [HttpPost(ApiUrlConstants.Accounts.ForgotPassword)]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.UserName);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return Ok();
                }

                // For more information on how to enable account confirmation and password reset please
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                // var callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme);
                // var callbackUrl = Request.Scheme + "://" + Request.Host + "/account/reset-password?userId=" + user.Id + "&code=" + code;
                var callbackUrl = GeneratePasswordResetLink(code);

                // await _emailSender.SendEmailAsync(model.Email, "Reset Password",
                //   $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
                await _userManagerService.SendForgotPasswordEmailAsync(_mapper.Map<UserModel>(user), callbackUrl);

                return Ok();
            }

            // If we got this far, something failed, redisplay form
            return BadRequest();
        }
        
        [HttpGet(ApiUrlConstants.Accounts.GetValuers)]
        [AllowAnonymous]
        public async Task<IEnumerable<ValuerViewModel>> GetValuers()
        {
            var valuers = await _userManager.GetUsersInRoleAsync(AV.Common.Constants.UserRoles.Valuer);
            return _mapper.Map<ICollection<ValuerViewModel>>(valuers);
        }
        
        [HttpGet(ApiUrlConstants.Accounts.GetAllUsers)]
        [AllowAnonymous]
        public async Task<IEnumerable<ValuerViewModel>> GetAllUsers()
        {
            var valuers = await _userManager.GetUsersInRoleAsync(AV.Common.Constants.UserRoles.Standard);
            return _mapper.Map<ICollection<ValuerViewModel>>(valuers);
        }

        [HttpPost(ApiUrlConstants.Accounts.PasswordReset)]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return Ok();
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet(ApiUrlConstants.Accounts.AllUserRolesDetailsGet)]
        [Authorize(ApiConstants.AuthenticationSchemes)]
        public IActionResult UserRoles([FromQuery] string policyName)
        {
            var userRoles = _roleManager.Roles.Select(r => new { r.Name });
            return Ok(new ApiResponse(HttpStatusCode.OK,
                data: userRoles));
        }

        private async Task<bool> IsValidUsernameAndPassword(string username, string password)
        {
            var user = await _userManager.FindByEmailAsync(username);
            return await _userManager.CheckPasswordAsync(user, password);
        }

        private User CreateUserModel(RegisterUserModel userModel)
        {
            return new User
            {
                UserName = userModel.Email,
                Email = userModel.Email,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                PhoneNumber = userModel.PhoneNumber
            };
        }

        private string GenerateEmailConfirmationLink(string userId, string code)
        {
            string url = Request.Scheme + "://" + Request.Host + Request.PathBase + ApiUrlConstants.Accounts.AccountConfirmation;
            var queryParams = new Dictionary<string, string>
            {
                {"userId", userId },
                {"code", code }
            };

            return QueryHelpers.AddQueryString(url, queryParams);
        }

        private string GeneratePasswordResetLink(string code)
        {
            string url = Request.Scheme + "://" + Request.Host + Request.PathBase + ApiUrlConstants.Accounts.PasswordReset;
            var queryParams = new Dictionary<string, string>
            {
                {"code", code }
            };

            return QueryHelpers.AddQueryString(url, queryParams);
        }
    }
}