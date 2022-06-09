using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace AV.Common.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Active { get; set; }
        public string ReturnUrl { get; set; }
        [DataType(DataType.Text)]
        [StringLength(80, MinimumLength = 4)]
        public string ReibNumber { get; set; }
        [DataType(DataType.Text)]
        [StringLength(80, MinimumLength = 10)]
        public string ReacNumber { get; set; }
        public string PasswordResetLink { get; set; }
        public DateTimeOffset PasswordResetExpiresOn { get; set; }
        public string IDNumber { get; set; }
        public ICollection<Account> Accounts { get; set; }
        public ICollection<Document> Documents { get; set; }
        public bool RegisteredProfessional { get; set; }
        public string LinkedInUrl { get; set; }
        public string IndustryExperience { get; set; }
        public virtual ICollection<UserRole> Roles { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        //public virtual ICollection<UserReferral> Referrals { get; set; }

    }

    public class UserDocument : Document { }
}