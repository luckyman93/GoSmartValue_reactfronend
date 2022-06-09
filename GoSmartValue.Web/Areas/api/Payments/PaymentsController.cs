using AutoMapper;
using AV.Common;
using AV.Common.DTOs;
using AV.Common.Entities;
using AV.Common.Interfaces.Services;
using AV.Contracts;
using AV.Contracts.Enums;
using AV.Contracts.Models.Payment.Commands;
using AV.Contracts.Models.Payment.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AV.Contracts.Models;
using UserModel = AV.Contracts.Models.Users.UserModel;

namespace GoSmartValue.Web.Areas.api.Payments
{
    [ApiController]
    [ApiTokenAuth]
    [Authorize(AuthenticationSchemes = ApiConstants.AuthenticationSchemes)]
    [Authorize(Policy = Constants.AccessPolicies.Admin)]

    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly UserManager<User> _userManager;
        private readonly IOptions<SmtpConfiguration> _smtpOptions;
        private readonly ILogger<PaymentsController> _logger;

        public PaymentsController(
            IMediator mediator,
            IMapper mapper,
            IEmailService emailService,
            UserManager<User> userManager,
            IOptions<SmtpConfiguration> smtpOptions,
            ILogger<PaymentsController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _emailService = emailService;
            _userManager = userManager;
            _smtpOptions = smtpOptions;
            _logger = logger;
        }

        [HttpPost(ApiUrlConstants.Payments.Response)]
        [AllowAnonymous]
        public async Task<ActionResult<PaymentModel>> PaymentResponse([FromQuery] SetPaymentToPaidCommand command)
        {
            try
            {
                // process DPO payment response
                var result = await _mediator.Send(command);

                if (result.Status == PaymentStatus.Paid
                    && command.Type == PaymentType.Subscription
                    && result.AccountId != default)
                {
                    await ActivateUserAccount(result);
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        private async Task<UserModel> ActivateUserAccount(PaymentModel result)
        {
            var userEntity = await _mediator.Send(new RenewAccountSubscriptionCommand
            {
                AccountId = result.AccountId,
            });
            //send activation email 
            SendConfirmationEmail(userEntity.User);
            return userEntity.User;
        }

        private async void SendConfirmationEmail(UserModel userModel)
        {
            try
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(_mapper.Map<User>(userModel));

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
