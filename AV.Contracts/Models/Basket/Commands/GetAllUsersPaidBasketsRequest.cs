using MediatR;
using System;
using System.Collections.Generic;

namespace AV.Contracts.Models.Basket.Commands
{
    public class GetAllUsersPaidBasketsRequest : IRequest<IEnumerable<BasketDto>>
    {
        public Guid UserId { get; set; }

        public GetAllUsersPaidBasketsRequest(Guid userId)
        {
            UserId = userId;
        }
    }
}