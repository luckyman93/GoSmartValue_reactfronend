using System;
using System.Collections.Generic;
using AV.Contracts.Enums;
using AV.Contracts.Models.Users;

namespace AV.Contracts.Models.Accounts.Subscriptions
{
    public class SubscriptionModel
    { 
        public Guid Id { get; set; }
        public UserDetailsModel User { get; set; }
        public DateTime EndDate { get; set; }
        public PackageModel PackageModel { get; set; }
        public AccountType AccountType { get; set; } = AccountType.Standard;
        public SubscriptionType SubscriptionType { get; set; } = SubscriptionType.Standard;
        public SubscriptionTerm Frequency { get; set; }
        public SubscriptionStatus Status { get; set; }
        public double Cost { get; set; }
        public bool Active { get; set; }
        public int InstantReportsLimit { get; set; }
        public int DetailedReportsLimit { get; set; }
        public ICollection<UserDetailsModel> Members { get; set; }
    }
}
