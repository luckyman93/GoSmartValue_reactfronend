using System;
using System.ComponentModel.DataAnnotations;

namespace GoSmartValue.Web.ViewModels
{
    public class RoleViewModel
    {
        [Required]
        public string RoleName { get; set; }

        public Guid Id { get; set; }
    }
}
