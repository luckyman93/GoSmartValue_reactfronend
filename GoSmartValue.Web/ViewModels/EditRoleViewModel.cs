using AV.Contracts.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoSmartValue.Web.ViewModels
{
    public class EditRoleViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "UserRole name is required")]
        public string RoleName { get; set; }
        public List<UserModel> Users { get; set; }
    }
}
