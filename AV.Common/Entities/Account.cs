using AV.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace AV.Common.Entities
{
    public class Account
    {
        public Account()
        {
            CompanyLogos = new List<CompanyLogoDocument>();
            BankAccounts = new List<BankAccount>();
            Transactions = new List<AccountTransaction>();
        }

        [Key] 
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public virtual Company Company { get; set; }
        public AccountType AccountType { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
        [ForeignKey("OrganisationId")]
        public virtual Organisation Organisation { get; set; }
        public Guid? OrganisationId { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool Active => ExpiryDate > DateTime.Now && VerifiedByUserId != default(Guid);
        public Guid VerifiedByUserId { get; set; }
        public bool IsValuer { get; set; }
        public bool IsSalesAgent { get; set; }
        public bool IsCorporate { get; set; }
        public bool IsBanker { get; set; }
        public bool IsInsurance { get; set; }
        public bool IsGovernmentAgency { get; set; }
        public bool IsDeveloper { get; set; }
        public string CompanyName { get; set; }

        //Standard User fields
        public int MaximumSubAccounts { get; set; }
        public double DiscountPerReferral { get; set; }
        public int InstantReportsLimit { get; set; }
        public int DetailedReportsLimit { get; set; }
        public int? SubscriptionOptionId { get; set; }
        [ForeignKey("SubscriptionOptionId")]
        public virtual SubscriptionOption SubscriptionOption { get; set; }
        public decimal Cost { get; set; }
        public PaymentFrequency Frequency { get; set; }
        public SubscriptionStatus Status { get; set; }
        public ICollection<User> Members { get; set; }
        public decimal Balance => CalculateBalance();
        public virtual ICollection<CompanyLogoDocument> CompanyLogos { get; set; }
        public virtual ICollection<BankAccount> BankAccounts { get; set; }
        public virtual ICollection<AccountTransaction> Transactions { get; set; }
        private decimal CalculateBalance()
        {
            var credits = Transactions.Where(t => t.IsCredit);
            var debits = Transactions.Where(t => !t.IsCredit);
            return credits.Sum(c => c.Amount) - debits.Sum(c => c.Amount);
        }
    }
}