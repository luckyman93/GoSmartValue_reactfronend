using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AV.Common.Entities
{
    public class BasketItem : BaseEntity
    {
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        [Key]
        public int Id { get; set; }
        [ForeignKey("Basket")]
        public int BasketId { get; set; }
        public Basket Basket { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string PromoCode { get; set; }
        public int? InputDataId { get; set; }
        public BasketItemInputData InputData { get; set; }

        public void Update(Guid currentUserId)
        {
            UpdatedDate = DateTimeOffset.UtcNow;
            UpdatedBy = currentUserId;
        }

        public void Update(BasketItem basketItem, Guid currentUserId)
        {
            UpdatedDate = DateTimeOffset.UtcNow;
            UpdatedBy = currentUserId;
            Discount = basketItem.Discount;
            InputData = basketItem.InputData;
            Price = basketItem.Price;
            PictureUrl = basketItem.PictureUrl;
            ProductId = basketItem.ProductId;
            Quantity = basketItem.Quantity;
        }
    }
}