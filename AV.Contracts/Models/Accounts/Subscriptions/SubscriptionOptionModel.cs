namespace AV.Contracts.Models.Accounts.Subscriptions
{
    public class SubscriptionOptionModel
    {
        public int Id { get; set; }
        public SubscriptionTerm Frequency { get; set; }
        public double Price { get; set; }
    }
}