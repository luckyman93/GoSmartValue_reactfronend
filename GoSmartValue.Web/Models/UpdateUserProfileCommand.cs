using System.ComponentModel.DataAnnotations;

namespace GoSmartValue.Web.Models
{
    public class UpdateUserProfileCommand
    {
        [Required, MinLength(2)]
        public string FirstName { get; set; }
        [Required, MinLength(2)]
        public string LastName { get; set; }
        [Required, MinLength(2)]
        public string PhoneNumber { get; set; }
    }
}
