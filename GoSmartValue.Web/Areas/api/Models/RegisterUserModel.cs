using System.ComponentModel.DataAnnotations;

namespace GoSmartValue.Web.Areas.api.Models
{
    public class RegisterUserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string IDNumber { get; set; }
        public string ReacNumber { get; set; }
        public string ReibNumber { get; set; }
        public string LinkedInUrl { get; set; }
        public bool RegisteredProfessional { get; set; }
    }
}
