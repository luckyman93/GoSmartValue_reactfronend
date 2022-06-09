using AV.Common.Interfaces;
using AV.Common.Interfaces.Repositories;
using AV.Contracts.Enums;
using AV.Contracts.Models.Accounts;
using AV.Contracts.Models.Payment.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoSmartValue.Web.Areas.api.Accounts
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = ApiConstants.AuthenticationSchemes)]
    public class SubscriptionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPaymentsRepository _payment;
        private readonly IComparableRepository _comparable;
        private readonly IInstructionRepository _instruction;

        public SubscriptionsController(IMediator mediator, IPaymentsRepository payment, IComparableRepository comparable, IInstructionRepository instruction)
        {
            _mediator = mediator;
            _payment = payment;
            _comparable = comparable;
            _instruction = instruction;
        }
        
        [HttpPost("VerifyPayment")]
        public async Task<IActionResult> VerifyPayment(SetPaymentToPaidCommand dpoRespond)
        {
            if (dpoRespond.TransactionToken == null)
            {
                return BadRequest(new ApiResponse<string>("No transaction", false, "Payment was not successfull"));
            }
            // process DPO payment success response
            var result = await _mediator.Send(dpoRespond);
            if (result.Status == PaymentStatus.Paid)
            {
              string response="Failed to update on paid reports";

                        if (dpoRespond.AccountId != null)
                            response = await _payment.ActivateStandardUserSubscription(dpoRespond.AccountId.Value);
                        if (dpoRespond.ComparableId != null)
                            response = await _comparable.UpdateComparablePaymentStatus(dpoRespond.ComparableId.Value);
                        if (dpoRespond.InstructionId != null)
                            response = await _instruction.UpdateInsructionPaymentStatus(dpoRespond.InstructionId.Value);

                        if (response == "saved")
                        {
                            return Ok(new ApiResponse<string>(response, true, "Account subscription activated"));
                        }
                        return BadRequest(new ApiResponse<string>(response, false, "Failed to activate account"));
            }
            return BadRequest(new ApiResponse<string>("DPO payment failed", false, "Payment was not successfull"));
        }
    }
}
