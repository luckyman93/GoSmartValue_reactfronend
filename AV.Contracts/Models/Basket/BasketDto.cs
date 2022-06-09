using System;
using System.Collections.Generic;
using AV.Contracts.Enums;

namespace AV.Contracts.Models.Basket
{
    public class BasketDto
    {
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }
        public DateTimeOffset? ConfirmedOn { get; set; }
        public int Id { get; set; }
        public int? InvoiceId { get; set; }
        public string OrderNo { get; set; }
        public string PromoCode { get; set; }
        public Guid BuyerId { get; set; }
        public decimal NetTotal { get; set; }
        public decimal DiscountTotal { get; set; }
        public decimal GrossTotal { get; set; }
        public BasketStatus Status { get; set; }
        public bool IsConfirmed => ConfirmedOn.HasValue && ConfirmedOn.Value > DateTimeOffset.UtcNow.AddHours(-24);

        public List<BasketItemDto> Items { get; set; } = new();

        public BasketDto()
        { }

        public BasketDto(Guid buyerId)
        {
            BuyerId = buyerId;
        }
    }
}
