using AutoMapper;
using AV.Common;
using AV.Common.Constants;
using AV.Common.DTOs;
using AV.Common.Entities;
using AV.Common.Interfaces;
using AV.Common.Interfaces.Services;
using AV.Contracts.Enums;
using AV.Contracts.Models;
using AV.Contracts.Models.Payment.Requests;
using AV.Contracts.Models.Product;
using AV.Contracts.Models.Product.Requests;
using AV.Contracts.Models.Users;
using AV.Contracts.Services;
using GoSmartValue.Web.Areas.api.Accounts;
using GoSmartValue.Web.Models;
using GoSmartValue.Web.Services;
using GoSmartValue.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationException = System.ApplicationException;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using User = AV.Common.Entities.User;

namespace GoSmartValue.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AccountController : SecureController
    {
        public IConfiguration Configuration { get; }
        private readonly SignInManager<User> _signInManager;
        private readonly IUserManagerService _userManagerService;
        private readonly IOptions<SmtpConfiguration> _smtpOptions;
        private readonly IDocumentStoreService _documentStoreService;
        private readonly IAccountsRepository _accountsRepository;
        private readonly IEmailService _emailService;
        private readonly ILogger<AccountsController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AccountController(UserManager<User> userManager,
            RoleManager<Role> roleManager,
            SignInManager<User> signInManager,
            ILogger<AccountsController> logger,
            IUserManagerService userManagerService,
            IOptions<SmtpConfiguration> smtpOptions,
            IDocumentStoreService documentStoreService,
            IAccountsRepository accountsRepository,
            IEmailService emailService,
            IConfiguration configuration,
            IUserManagerService usersService,
            IMediator mediator,
            IMapper mapper
        )
            : base(userManager, roleManager, usersService, mapper)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManagerService = userManagerService;
            _smtpOptions = smtpOptions;
            _documentStoreService = documentStoreService;
            _accountsRepository = accountsRepository;
            _emailService = emailService;
            Configuration = configuration;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(Guid reference = default, [FromQuery] string returnUrl = null)
        {
            if (!Url.IsLocalUrl(returnUrl))
            {
                returnUrl = ViewBag.ReturnUrl ?? returnUrl;
            }

            var userModel = new RegisterUserViewModel
            {
                ComparableReference = reference,
                ReturnUrl = returnUrl
            };

            ViewBag.ReturnUrl = userModel.ReturnUrl;
            return View(userModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(AV.Contracts.Models.Users.UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(userModel);
                user.Active = true;
                user.EmailConfirmed = true;
                var result = await UserManager.CreateAsync(user, userModel.Password);

                if (result.Succeeded)
                {
                    await AddRolesToUser(user, false, false, false, true);
                    _userManagerService.CreateUserAccountRecord(user, SubscriptionType.Standard);
                    //Sign - User In this time
                    await _signInManager.SignInAsync(user, false);
                    //Activation required next time - send activation link
                    SendConfirmationEmail(user);

                    if (!string.IsNullOrEmpty(userModel.ReturnUrl) && Url.IsLocalUrl(userModel.ReturnUrl))
                    {
                        return Redirect(user.ReturnUrl);
                    }
                    //Redirect to account creation confirmation page
                    return RedirectToAction("RegistrationSuccessful", "Home");
                }
                foreach (var identityError in result.Errors)
                {
                    ModelState.AddModelError(identityError.Code, identityError.Description);
                }
            }
            return View(userModel);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/account/confirm")]
        public async Task<IActionResult> EmailConfirmation(string email, string token)
        {
            if (email == null || token == null)
            {
                return Redirect(Constants.LandingPageUrlDefault);
            }

            var user = await UserManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with Username '{email}'.");
            }

            await UserManager.ConfirmEmailAsync(user, token);
            var redirectUrl = await ResolveUserRedirectUrl(new UserLoginViewModel
            {
                UserName = user.UserName,
                Active = user.Active
            });
            return Redirect(redirectUrl);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterValuer()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterValuer(RegisterValuerViewModel clientModel)
        {
            if (ModelState.IsValid &&
                (clientModel.ValuerSingleUse || clientModel.ValuerBusiness || clientModel.ValuerFastTrack))
            {
                var user = _mapper.Map<User>(clientModel);
                user.Active = true;

                await SaveDocument(clientModel.IDDocument, user);

                var result = await UserManager.CreateAsync(user, clientModel.Password);

                if (result.Succeeded)
                {
                    await AddRolesToUser(user, clientModel.Valuer, clientModel.SalesAgent, false, false);

                    var subscriptionType = ResolveSubscriptionType(clientModel);
                    _userManagerService.CreateUserAccountRecord(user, subscriptionType, clientModel.Valuer, clientModel.SalesAgent);
                    // start payment proccess
                    var request = await CreateMakePaymentRequest(user, subscriptionType);
                    return RedirectToAction("Payment", "Payment", request);
                }

                foreach (var identityError in result.Errors)
                {
                    ModelState.AddModelError(identityError.Code, identityError.Description);
                    ModelState.AddModelError("Error", identityError.Description);
                }
            }

            return View("RegisterValuer", clientModel);
        }




        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterCorporate()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterCorporate(RegisterCorporateViewModel clientModel)
        {
            if (ModelState.IsValid &&
                (clientModel.BlackAccount || clientModel.GreenAccount))
            {
                var user = _mapper.Map<User>(clientModel);
                user.Active = true;

                await SaveDocument(clientModel.IDDocument, user);
                var result = await UserManager.CreateAsync(user, clientModel.Password);

                if (result.Succeeded)
                {
                    await AddRolesToUser(user, false, false, true, false);
                    var subscriptionType = ResolveSubscriptionType(clientModel);
                    _userManagerService.CreateUserAccountRecord(user, subscriptionType, false, false,
                       true, clientModel.Banker, clientModel.Insurance, clientModel.GovernmentAgency, clientModel.Developer, clientModel.CompanyName);

                    //await _signInManager.SignInAsync(user, clientModel.RememberMe);

                    // start payment proccess
                    var request = await CreateMakePaymentRequest(user, subscriptionType);
                    return RedirectToAction("Payment", "Payment", request);
                }

                foreach (var identityError in result.Errors)
                {
                    ModelState.AddModelError(identityError.Code, identityError.Description);
                    ModelState.AddModelError("Error", identityError.Description);
                }
            }

            return View("RegisterCorporate", clientModel);
        }

        private async Task<ProductModel> GetProductPrice(string productName)
        {
            return await _mediator.Send(new GetProductDetailsRequest
            {
                Name = productName,
            });
        }

        private SubscriptionType ResolveSubscriptionType(RegisterValuerViewModel clientModel)
        {
            if (clientModel.ValuerBusiness)
                return SubscriptionType.Business;
            if (clientModel.ValuerFastTrack)
                return SubscriptionType.FastTrack;
            if (clientModel.ValuerSingleUse)
                return SubscriptionType.SingleUse;
            return SubscriptionType.Unknown;
        }

        private SubscriptionType ResolveSubscriptionType(RegisterCorporateViewModel clientModel)
        {
            if (clientModel.BlackAccount)
                return SubscriptionType.Black;
            if (clientModel.GreenAccount)
                return SubscriptionType.Green;
            return SubscriptionType.Unknown;
        }

        private async Task<MakePaymentRequest> CreateMakePaymentRequest(User user, SubscriptionType subscriptionType)
        {
            var product = await GetProductPrice(subscriptionType.ToString());
            Guid accountId = default;

            if (user.Accounts.Count == 0)
            {
                var account = await _accountsRepository.GetUserAccount(user.Id);
                accountId = account.Id;
            }

            return new MakePaymentRequest
            {
                InitiatedByUserId = user.Id,
                AccountId = accountId,
                Reference = user.Id.ToString(),
                Currency = Constants.Currency.Botswana.Code,
                Type = PaymentType.Subscription,
                Amount = (int)product.Price,
                ServiceName = product.Description,
                ServiceType = product.ServiceType
            };
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/Account/Login")]
        public IActionResult Login([FromQuery] string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                ViewBag.ReturnUrl = returnUrl;
            }

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/Account/Logout")]
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginViewModel userModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(userModel.UserName, userModel.Password,
                    userModel.RememberMe, false);
                if (result.Succeeded)
                {
                    var hasActiveAccount = _userManagerService.HasActiveAccount(userModel.UserName);

                    if (!hasActiveAccount)
                    {
                        var user = await UserManager.FindByEmailAsync(userModel.UserName);
                        var userRoles = await UserManager.GetRolesAsync(user);

                        if (userRoles.Contains(UserRoles.Valuer))
                        {
                            //Redirect to renew account page based on role of account
                        }
                        if (userRoles.Contains(UserRoles.Corporate))
                        {
                            //Redirect to renew account page based on role of account
                        }

                        _userManagerService.CreateUserAccountRecord(user, SubscriptionType.Standard);
                    }

                    _logger.LogInformation($"User '{userModel.UserName}' is now logged in.");

                    var returnUrl = await ResolveUserRedirectUrl(userModel);
                    return Redirect(returnUrl);
                }

                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(userModel);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPassword)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(forgotPassword.UserName);
                if (user != null)
                {
                    var token = await UserManager.GeneratePasswordResetTokenAsync(user);
                    var resetLink = Url.Action("ResetPassword", "Account"
                        , new { email = user.Email, token },
                        Request.Scheme,
                        Startup.EnvironmentVariables["Hostname"].ToString());
                    //var resetLink =
                    //    $"{HttpContext.Request.Scheme}://{Startup.EnvironmentVariables["Hostname"]}/Account/ResetPassword?token={HttpUtility.UrlEncode(token)}&email={user.Email}";
                    _userManagerService.SavePasswordResetLink(user.Id, resetLink);
                    await _emailService.SendMail(user.Email, "Account Password Reset - goSmartValue.com", null,
                        _smtpOptions.Value,
                        Constants.FromAddress,
                        new EmailTemplate
                        {
                            Data = new Dictionary<string, string>()
                            {
                                {"resetLink", resetLink},
                                {"logoImageUr", Url.Content("~/gosmartvalue.png")},
                                {"firstName", user.FirstName},
                                {"fastName", user.LastName}
                            },
                            Template = TemplateConstants.TemplateAccountPasswordReset
                        });
                    _logger.LogInformation($"Password reset link for '{user.Email}': {resetLink}");

                    return View("ForgotPasswordConfirmation");
                }
            }

            return View("ForgotPasswordConfirmation");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                ModelState.AddModelError("", $"you must provide both Username and reset token.");
                return View(email, token);
            }
            var resetModel = new ResetPasswordViewModel
            {
                Token = token,
                Email = email
            };
            return View(resetModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await UserManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return View("ResetPasswordConfirmation");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View(model);
                }
            }

            return View();
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        private async Task SaveDocument(IFormFile document, User user)
        {
            if (document != null)
            {
                var savedDocument = _mapper.Map<Document>(await _documentStoreService.SaveFile(document));
                switch (user.Documents)
                {
                    case null:
                        user.Documents = new List<Document> { savedDocument };
                        break;
                    default:
                        user.Documents.Add(savedDocument);
                        break;
                }

                user.Documents.Add(savedDocument);
            }
        }

        private async Task AddRolesToUser(User user, bool valuer, bool salesAgent, bool corporate, bool standard)
        {
            if (standard)
            {
                await _signInManager.UserManager.AddToRoleAsync(user, AV.Common.Constants.UserRoles.Standard);
            }
            if (valuer)
            {
                await _signInManager.UserManager.AddToRoleAsync(user, AV.Common.Constants.UserRoles.Valuer);
            }

            if (salesAgent)
            {
                await _signInManager.UserManager.AddToRoleAsync(user, AV.Common.Constants.UserRoles.SalesAgent);
            }

            if (corporate)
            {
                await _signInManager.UserManager.AddToRoleAsync(user, AV.Common.Constants.UserRoles.Corporate);
            }
        }

        private async void SendConfirmationEmail(User userModel)
        {
            try
            {
                var token = await UserManager.GenerateEmailConfirmationTokenAsync(userModel);

                var confirmationEmailUrl = Url.Action("EmailConfirmation", "Account"
                    , new { email = userModel.Email, token },
                    Request.Scheme,
                    Startup.EnvironmentVariables["Hostname"].ToString());

                _logger.LogInformation($"ConfirmationEmailLink: {confirmationEmailUrl}");
                await _emailService.SendMail(userModel.Email, "Email Confirmation - goSmartValue.com", null,
                    _smtpOptions.Value,
                    null,
                    new EmailTemplate
                    {
                        Data = new Dictionary<string, string>()
                        {
                            {"activationLink", confirmationEmailUrl},
                            {"logoImageUr", Url.Content("~/gosmartvalue.png")},
                            {"firstName", userModel.FirstName},
                            {"fastName", userModel.LastName}
                        },
                        Template = TemplateConstants.TemplateAccountActivation
                    });
            }
            catch (Exception exception)
            {
                _logger.LogError($"Unable send email for account activation for User:{userModel.Id}: Error: {exception.Message}");
            }
        }
    }
}