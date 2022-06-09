using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AV.Contracts.Enums;

namespace AV.Common.Entities
{
    public class Basket: BaseEntity
    {
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        [Key]
        public int Id { get; set; }
        public Guid? PaymentId { get; set; }
        public string OrderNo { get; set; }
        public string PromoCode { get; set; }
        public Guid BuyerId { get; set; }
        public decimal NetTotal { get; set; }
        public decimal DiscountTotal { get; set; }
        public decimal GrossTotal { get; set; }
        public BasketStatus Status { get; set; }
        public List<BasketItem> Items { get; set; }
        public DateTimeOffset? ConfirmedOn { get; set; }

        public Basket()
        {
            Status = BasketStatus.Draft;
            CreatedDate = DateTimeOffset.UtcNow;
            UpdatedDate = DateTimeOffset.UtcNow;
            Items = new List<BasketItem>();
        }

        public Basket(Guid buyerId)
        {
            Items = new List<BasketItem>();
            BuyerId = buyerId;
            Status = BasketStatus.Draft;
            CreatedDate = DateTimeOffset.UtcNow;
            UpdatedDate = DateTimeOffset.UtcNow;
            CreatedBy = buyerId;
            UpdatedBy = buyerId;
        }

        public void RecalculateTotals()
        {
            NetTotal = Items.Sum(b => b.Price);
            DiscountTotal = Items.Sum(b => b.Discount);
            GrossTotal = NetTotal - DiscountTotal;
        }
    }
}
