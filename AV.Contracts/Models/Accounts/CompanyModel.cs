using System;
using System.Collections.Generic;

namespace AV.Contracts.Models.Accounts
{
    public class CompanyModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CompanyType Type { get; set; }
        public string LogoUrl { get; set; }
        public string Description { get; set; }
        public string RegistrationNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Account> SubscriptionAccounts { get; set; }
    }
}
