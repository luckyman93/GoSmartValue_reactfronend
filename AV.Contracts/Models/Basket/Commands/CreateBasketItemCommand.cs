namespace AV.Contracts.Models.Basket.Commands
{
    public class CreateBasketItemCommand
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string PromoCode { get; set; }
        public CreateBasketItemInputDataCommand InputData { get; set; }
    }
}