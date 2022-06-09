using System;

namespace AV.Contracts.Models.Basket
{
    public class BasketItemDto
    {
        public DateTimeOffset CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public Guid UpdatedBy { get; set; }
        public int Id { get; set; }
        public int BasketId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string PromoCode { get; set; }
        public int? InputDataId { get; set; }
        public BasketItemInputDataDto InputData { get; set; }
    }
}