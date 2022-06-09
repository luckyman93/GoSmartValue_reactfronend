using AV.Contracts;
using AV.Contracts.Models.Basket.Commands;
using GoSmartValue.Web.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AV.Common;
using AV.Common.DTOs;
using AV.Contracts.Models.Basket;
using AV.Infrastructure.Services.PaymentGateways.Cellulant;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace GoSmartValue.Web.Areas.api.Basket
{
    [ApiTokenAuth]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BasketController : ControllerBase
    {
        private readonly ILogger<BasketController> _logger;
        private readonly IMediator _mediator;
        private readonly IUserManagerService _userManagerService;

        public BasketController(
            ILogger<BasketController> logger,
            IMediator mediator,
            IUserManagerService userManagerService)
        {
            _logger = logger;
            _mediator = mediator;
            _userManagerService = userManagerService;
        }

        [HttpPost(ApiUrlConstants.Basket.Items.POST_BasketItems)]
        public async Task<IActionResult> AddBasketItems([FromBody] List<CreateBasketItemCommand> basketItemsCommands)
        {
            var userId = _userManagerService.GetLoggedInUserIdAsync();
            return Created(nameof(AddBasketItems), await _mediator.Send(new AddBasketItemsCommand(userId, basketItemsCommands)));
        }


        [HttpGet(ApiUrlConstants.Basket.GET_CurrentBasket)]
        public async Task<IActionResult> GetCurrentBasket()
        {
            try
            {
                var userId = _userManagerService.GetLoggedInUserIdAsync();
                return Ok(await _mediator.Send(new GetCurrentUserBasketRequest(userId)));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Unable to get Basket for user.", exception);
                return BadRequest();
            }
        } 

        [HttpGet(ApiUrlConstants.Basket.GET_CurrentUsersPaidBaskets)]
        public async Task<IActionResult> GetAllUsersPaidBaskets()
        {
            try
            {
                var userId = _userManagerService.GetLoggedInUserIdAsync();
                return Ok(await _mediator.Send(new GetAllUsersPaidBasketsRequest(userId)));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Unable to get Baskets for user.", exception);
                return BadRequest();
            }

        } 
        
        [HttpPut(ApiUrlConstants.Basket.Items.PUT_BasketItemByID)]
        public async Task<IActionResult> GetCurrentBasket([FromRoute] int id, UpdateBasketItemCommand updateBasketItemCommand)
        {
            try
            {
                var userId = _userManagerService.GetLoggedInUserIdAsync();
                updateBasketItemCommand.UserId = userId;
                updateBasketItemCommand.Id = id;
                return Ok(await _mediator.Send(updateBasketItemCommand));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Unable to amend Basket Item for user.", exception);
                return BadRequest();
            }

        }

        [HttpPut(ApiUrlConstants.Basket.PUT_ConfirmCurrentBasket)]
        public async Task<IActionResult> GetCurrentBasket([FromBody] ConfirmBasketCommand confirmBasketCommand)
        {
            try
            {
                var userId = _userManagerService.GetLoggedInUserIdAsync();
                confirmBasketCommand.UserId = userId;

                return Ok(await _mediator.Send(confirmBasketCommand));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Unable to confirm Basket for user.", exception);
                return BadRequest();
            }

        }

        [HttpPut(ApiUrlConstants.Basket.PUT_GeneratePaymentToken)]
        public async Task<IActionResult> GeneratePaymentToken([FromBody] GenerateBasketPaymentTokenCommand generateBasketPaymentTokenCommand)
        {
            try
            {
                var userId = _userManagerService.GetLoggedInUserIdAsync();
                generateBasketPaymentTokenCommand.UserId = userId;

                return Ok(await _mediator.Send(generateBasketPaymentTokenCommand));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Unable to generate payment token for current users Basket.", exception);
                return BadRequest();
            }
        }

        // [HttpPut(ApiUrlConstants.Basket.PUT_GeneratePaymentToken)]
        // public async void SendEmail([FromBody] GenerateBasketPaymentTokenCommand generateBasketPaymentTokenCommand)
        // {
        //    
        //         try
        //         {
        //             var token = await UserManager<>.GenerateEmailConfirmationTokenAsync(_mapper.Map<User>(userModel));
        //
        //             var confirmationEmailUrl = Url.Action("EmailConfirmation", "Account"
        //                 , new { email = userModel.Email, token },
        //                 Request.Scheme,
        //                 Startup.EnvironmentVariables["Hostname"].ToString());
        //
        //             _logger.LogInformation($"ConfirmationEmailLink: {confirmationEmailUrl}");
        //             await _emailService.SendMail("oaitse@gosmartvalue.", "Email Confirmation - goSmartValue.com", null,
        //                 _smtpOptions.Value,
        //                 null,
        //                 new EmailTemplate
        //                 {
        //                     Data = new Dictionary<string, string>()
        //                     {
        //                         {"activationLink", confirmationEmailUrl},
        //                         {"logoImageUr", Url.Content("~/gosmartvalue.png")},
        //                         {"firstName", userModel.FirstName},
        //                         {"fastName", userModel.LastName}
        //                     },
        //                     Template = TemplateConstants.TemplateAccountActivation
        //                 });
        //         }
        //         catch (Exception exception)
        //         {
        //             _logger.Log(LogLevel.Error,
        //                 $"Unable send email for account activation for User:{userModel.Id}: Error: {exception.Message}");
        //         }
        //     
        // }

        [HttpDelete(ApiUrlConstants.Basket.Items.DELETE_BasketItems)]
        public async Task<IActionResult> DeleteBasketItem([FromRoute] int id)
        {
            try
            {
                var userId = _userManagerService.GetLoggedInUserIdAsync();
                return Ok(await _mediator.Send(new DeleteBasketItemCommand(userId,id)));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Unable to delete BasketItem for user.", exception);
                return BadRequest();
            }

        }
    }
}
