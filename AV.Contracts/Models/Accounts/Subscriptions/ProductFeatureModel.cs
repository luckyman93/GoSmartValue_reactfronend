namespace AV.Contracts.Models.Accounts.Subscriptions
{
    public class ProductFeatureModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
    }
}