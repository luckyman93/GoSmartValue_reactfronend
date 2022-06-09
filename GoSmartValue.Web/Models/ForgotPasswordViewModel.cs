using System.ComponentModel.DataAnnotations;

namespace GoSmartValue.Web.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }
    }
}