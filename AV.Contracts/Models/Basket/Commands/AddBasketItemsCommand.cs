using MediatR;
using System;
using System.Collections.Generic;

namespace AV.Contracts.Models.Basket.Commands
{
    public class AddBasketItemsCommand : IRequest<BasketDto>
    {
        public AddBasketItemsCommand(Guid userId, List<CreateBasketItemCommand> items)
        {
            UserId = userId;
            Items = items;
        }

        public Guid UserId { get; set; }
        public List<CreateBasketItemCommand> Items { get; set; }
    }
}
