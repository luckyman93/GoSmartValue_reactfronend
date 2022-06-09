using System;
using AV.Contracts.Enums;

namespace AV.Contracts.Models.Accounts
{
    public class AccountViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public virtual Users.UserModel User { get; set; }
        public UserAccountType AccountType { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool Active { get; set; }
        public Guid? VerifiedByUserId { get; set; }
        public string CompanyName { get; set; }
    }
}