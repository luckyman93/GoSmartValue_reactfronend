using MediatR;
using System;

namespace AV.Contracts.Models.Accounts.Commands
{
    public  class GetUserSubscriptionRequest:IRequest<UserActiveSubscriptionViewModel>
    {
        public GetUserSubscriptionRequest(Guid userId)
        {
            UserId = userId;
        }
        public Guid UserId { get; private set; }
    }

}
