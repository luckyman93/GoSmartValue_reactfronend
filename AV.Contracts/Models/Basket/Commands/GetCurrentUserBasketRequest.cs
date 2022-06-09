using MediatR;
using System;

namespace AV.Contracts.Models.Basket.Commands
{
    public class GetCurrentUserBasketRequest : IRequest<BasketDto>
    {
        public Guid UserId { get; set; }

        public GetCurrentUserBasketRequest(Guid userId)
        {
            UserId = userId;
        }
    }
}