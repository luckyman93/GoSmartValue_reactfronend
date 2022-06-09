using AV.Contracts.Enums;
using AV.Contracts.Models.Accounts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AV.Contracts.Models.Users
{
    public class UserModel: UserLoginViewModel
    {
        public Guid Id { get; set; }
        [Required][DataType(DataType.Text)][StringLength(80, MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required] [DataType(DataType.Text)] [StringLength(80, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required][Display(Name = "Confirm password")][DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string PasswordConfirm { get; set; }
  
        public List<UserRolesViewModel> Roles { get; set; }
        public List<UserClaimsViewModel> Claims { get; set; }
        public string Email
        {
            get { return UserName; }
        }

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
        public ICollection<AccountViewModel> Accounts { get; set; }
        public ICollection<UserAccountType> AccountTypes { get; set; }
    }
}
