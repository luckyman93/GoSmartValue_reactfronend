using System;
using MediatR;

namespace AV.Contracts.Models.Basket.Commands
{
    public class DeleteBasketItemCommand : IRequest<BasketDto>
    {
        public Guid UserId { get; set; }
        public int BasketItemId { get; set; }

        public DeleteBasketItemCommand(Guid userId, int basketItemId)
        {
            UserId = userId;
            BasketItemId = basketItemId;
        }
    }
}