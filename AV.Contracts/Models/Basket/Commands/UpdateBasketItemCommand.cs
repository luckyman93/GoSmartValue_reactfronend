using System;
using MediatR;
using Newtonsoft.Json;

namespace AV.Contracts.Models.Basket.Commands
{
    public class UpdateBasketItemCommand : IRequest<BasketDto>
    {
        [JsonIgnore]
        public Guid? UserId { get; set; }
        [JsonIgnore]
        public int? Id { get; set; } // BasketItemId
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string PromoCode { get; set; }
        public BasketItemInputDataDto InputData { get; set; }
    }
}