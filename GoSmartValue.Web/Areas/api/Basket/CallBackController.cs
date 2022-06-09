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
using AV.Contracts.Models.Basket;
using AV.Infrastructure.Services.PaymentGateways.Cellulant;
using Microsoft.Extensions.Logging;

namespace GoSmartValue.Web.Areas.api.Basket
{
    [ApiController]
    public class CallBackController : ControllerBase
    {
        private readonly ILogger<BasketController> _logger;
        private readonly IMediator _mediator;
        private readonly IUserManagerService _userManagerService;

        public CallBackController(
            ILogger<BasketController> logger,
            IMediator mediator,
            IUserManagerService userManagerService)
        {
            _logger = logger;
            _mediator = mediator;
            _userManagerService = userManagerService;
        }

        
        [HttpPost(ApiUrlConstants.CallBack.POST_SUCCESS)]
        public async Task<IActionResult> ProcessSuccessFullPayment([FromBody] CellulantCallBackRequestDTO cellulantCallBackRequestDto)
        {
            try
            {
                var userId = _userManagerService.GetLoggedInUserIdAsync();
                // processSuccessFullPaymentCommand.UserId = userId;
                Response.Redirect("/");

                return Ok(new SucceFullPaymentDTO()
                {
                    checkoutRequestID = cellulantCallBackRequestDto.checkoutRequestID,
                    merchantTransactionID = cellulantCallBackRequestDto.merchantTransactionID,
                    statusCode = cellulantCallBackRequestDto.requestStatusCode,
                    statusDescription = cellulantCallBackRequestDto.requestStatusDescription,
                    receiptNumber = "GOS-001"
                });
            }
            catch (Exception exception)
            {
                _logger.LogError($"Unable to payment.", exception);
                return BadRequest();
            }
        }


    }
}
