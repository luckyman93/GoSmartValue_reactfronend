using AV.Contracts.Models.Payment.Responses;
using MediatR;
using System;


namespace AV.Contracts.Models.Payment.Commands
{
	public class RenewAccountSubscriptionCommand : IRequest<RenewAccountSubscriptionResponse>
	{
        public RenewAccountSubscriptionCommand()
        { }

        public RenewAccountSubscriptionCommand(Guid accountId)
        {
            AccountId = accountId;
        }
        public Guid AccountId { get; set; }
	}
}
