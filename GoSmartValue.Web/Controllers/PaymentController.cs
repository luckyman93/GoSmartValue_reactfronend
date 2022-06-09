using AutoMapper;
using AV.Common;
using AV.Common.DTOs;
using AV.Common.Entities;
using AV.Common.Interfaces;
using AV.Common.Interfaces.Services;
using AV.Contracts;
using AV.Contracts.Enums;
using AV.Contracts.Models;
using AV.Contracts.Models.Payment.Commands;
using AV.Contracts.Models.Payment.Models;
using AV.Contracts.Models.Payment.Requests;
using GoSmartValue.Web.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User = AV.Common.Entities.User;

namespace GoSmartValue.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class PaymentController : SecureController
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IAccountsRepository _accountsRepository;
        private readonly IEmailService _emailService;
        private readonly IOptions<SmtpConfiguration> _smtpOptions;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        private readonly ConfigurationDpo _dpoConfiguration;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IAccountsRepository accountsRepository,
            ILogger<PaymentController> logger,
            IOptions<ConfigurationDpo> configOptions,
            IEmailService emailService,
            IOptions<SmtpConfiguration> smtpOptions,
            IUserManagerService userManagerService,
            IMediator mediator,
            IMapper mapper) : base(userManager, roleManager, userManagerService, mapper)
        {
            _logger = logger;
            _signInManager = signInManager;
            _accountsRepository = accountsRepository;
            _emailService = emailService;
            _smtpOptions = smtpOptions;
            _mediator = mediator;
            _mapper = mapper;
            _dpoConfiguration = configOptions.Value;
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> PaymentSuccess([FromQuery] SetPaymentToPaidCommand command)
        {
            // process DPO payment success response
            var result = await _mediator.Send(command);

            if (result.Status == PaymentStatus.Paid)
            {
                switch (command.Type)
                {
                    //Account Registration
                    case PaymentType.Subscription when result.AccountId != default:
                        {
                            var user = await ActivateUserAccount(result);
                            await _signInManager.SignInAsync(_mapper.Map<User>(user), false);
                        }

                        return RedirectToAction("RegistrationSuccessful", "Home");
                    //Instant Report
                    case PaymentType.InstantReport when result.Reference != default:
                        return RedirectToAction("InstantReport", "Document", new { id = command.Reference });
                    // Detailed Report
                    case PaymentType.DetailedReport when result.Reference != default:
                        return RedirectToAction("ValuationReport", "Document", new { id = command.Reference });
                }
            }

            return View(result);
        }



        [HttpGet]
        [AllowAnonymous]
        [Route(Constants.WebsiteRoutes.Payment.RequestPayment)]
        public async Task<IActionResult> Payment([FromQuery] SetProductAmountRequest request)
        {
            var makePayment = await _mediator.Send(request);
            return View(makePayment);
        }

        [HttpGet]
        public IActionResult AddCredit(AddCreditCommand account)
        {
            return View(account);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route(Constants.WebsiteRoutes.Payment.RequestPayment)]
        public async Task<IActionResult> MakePayment(MakePaymentRequest request)
        {
            if (ModelState.IsValid)
            {
                await SetPaymentDefaults(request);
                //call handler
                var paymentResponse = await _mediator.Send(request);

                // redirect to DPO
                return Redirect($"{_dpoConfiguration.PaymentPageUrl}{paymentResponse.TransToken}");
            }
            return View("Payment", request);
        }

        public string GenerateReference(PaymentType paymentType)
        {
            return
                $"GO{paymentType.ToString().Substring(0, 2).ToUpper()}{DateTimeOffset.UtcNow:yyyyMMdd}{GenerateRandomString(4)}";
        }

        public static string GenerateRandomString(int length)
        {
            var random = new Random();
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }
            return result.ToString();
        }

        private async Task SetPaymentDefaults(MakePaymentRequest request)
        {
            //Set Account and User Id
            if (request.InitiatedByUserId == default)
                request.InitiatedByUserId = CurrentUser.Id;

            if (request.AccountId == default)
            {
                var account = await _accountsRepository.GetUserAccount(CurrentUser.Id);
                request.AccountId = account.Id;
            }

            if (string.IsNullOrEmpty(request.Reference))
            {
                request.Reference = GenerateReference(request.Type);
            }

            //Service Name
            if (string.IsNullOrEmpty(request.ServiceName))
            {
                request.ServiceName = "GOsmartvalue.com";
            }
            request.ReturnUrl = Url.Action("PaymentSuccess", "Payment", new { reference = request.Reference, type = request.Type }, Request.Scheme);
            request.BackUrl = Url.Action("Payment", "Payment", new { request.Reference, request.Type, request.ServiceName, request.Amount, request.InitiatedByUserId, request.Currency, request.AccountId }, Request.Scheme);
        }

        private async Task<AV.Contracts.Models.Users.UserModel> ActivateUserAccount(PaymentModel result)
        {
            var userEntity = await _mediator.Send(new RenewAccountSubscriptionCommand
            {
                AccountId = result.AccountId,
            });
            //send activation email 
            SendConfirmationEmail(userEntity.User);
            return userEntity.User;
        }

        private async void SendConfirmationEmail(AV.Contracts.Models.Users.UserModel userModel)
        {
            try
            {
                var token = await UserManager.GenerateEmailConfirmationTokenAsync(_mapper.Map<User>(userModel));

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
                _logger.Log(LogLevel.Error,
                    $"Unable send email for account activation for User:{userModel.Id}: Error: {exception.Message}");
            }
        }
    }
}