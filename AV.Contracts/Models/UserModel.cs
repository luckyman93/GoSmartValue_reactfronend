using AV.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace AV.Contracts.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [StringLength(80, MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [StringLength(80, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string PasswordConfirm { get; set; }
        public List<Claim> Claims { get; set; }
        public string Email
        {
            get { return UserName; }
        }
        public string UserName { get; set; }
        public virtual bool EmailConfirmed { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public virtual bool PhoneNumberConfirmed { get; set; }
        public virtual bool TwoFactorEnabled { get; set; }
        public virtual DateTimeOffset? LockoutEnd { get; set; }
        public virtual bool LockoutEnabled { get; set; }
        public virtual int AccessFailedCount { get; set; }
        public string PasswordResetLink { get; set; }
        public DateTimeOffset PasswordResetExpiresOn { get; set; }

        public ICollection<UserAccountType> AccountTypes { get; set; }
    }
}
