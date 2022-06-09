using System.ComponentModel.DataAnnotations;

namespace GoSmartValue.Web.Models
{
    public class ResetPasswordViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password must match.")]
        public string PasswordConfirm { get; set; }
        public string Token { get; set; }
    }
}
