namespace AV.Contracts.Models.Accounts.Subscriptions
{
    public class PackageFeatureModel
    {
        public int Id { get; set; }
        public int PackageId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
    }
}