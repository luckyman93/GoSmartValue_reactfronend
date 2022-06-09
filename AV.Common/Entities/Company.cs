using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AV.Contracts.Enums;

namespace AV.Common.Entities
{
    public class Company
    {
        public Company()
        {
            SubscriptionAccounts = new List<Account>();
        }

        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CompanyType Type { get; set; }
        public string LogoUrl { get; set; }
        public string Description { get; set; }
        public string RegistrationNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public virtual ICollection<Account> SubscriptionAccounts { get; set; }
    }
}