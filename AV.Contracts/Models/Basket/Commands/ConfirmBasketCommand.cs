using MediatR;
using System;

namespace AV.Contracts.Models.Basket.Commands
{
    public class ConfirmBasketCommand : IRequest<BasketDto>
    {
        public Guid UserId { get; set; }
    }
}
