using AV.Contracts.Enums;
using System;

namespace AV.Contracts.Models.Accounts
{
    public class UserActiveSubscriptionViewModel
    {
        public Guid Id { get; set; }
        public int MaximumSubAccounts { get; set; }
        public double DiscountPerReferral { get; set; }
        public int InstantReportsLimit { get; set; }
        public int DetailedReportsLimit { get; set; }
        public PaymentFrequency Frequency { get; set; }
        public int? SubscriptionOptionId { get; set; }
        public SubscriptionStatus Status { get; set; }
    }
}
