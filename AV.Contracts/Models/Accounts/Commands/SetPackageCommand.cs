using MediatR;
using System;

namespace AV.Contracts.Models.Accounts.Commands
{
    public class SetPackageCommand:IRequest<PaymentTrack>
    {
        public SetPackageCommand(int subscriptionOptionId, Guid userId)
        {
            SubscriptionOptionId = subscriptionOptionId;
            UserId = userId;
        }
        public int SubscriptionOptionId { get; private set; }
        public Guid UserId { get; private set; }
    }
}
