using AV.Contracts.Enums;
using System;
using System.Collections.Generic;

namespace AV.Contracts.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public virtual UserModel User { get; set; }
        public UserAccountType AccountType { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
        public Organization Organization { get; set; }
        public Guid? OrganisationId { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool Active { get; set; }
        public Guid? VerifiedByUserId { get; set; }
        public string CompanyName { get; set; }
        public IEnumerable<BankAccount> BankDetails { get; set; }
        public ICollection<CompanyLogoDocumentModel> CompanyLogos { get; set; }
    }
}