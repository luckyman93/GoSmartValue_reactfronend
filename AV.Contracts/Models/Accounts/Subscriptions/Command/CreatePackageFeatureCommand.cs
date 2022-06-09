namespace AV.Contracts.Models.Accounts.Subscriptions.Command
{
    public class CreatePackageFeatureCommand
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
    }
}