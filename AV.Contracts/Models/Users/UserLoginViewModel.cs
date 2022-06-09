using System.ComponentModel.DataAnnotations;

namespace AV.Contracts.Models.Users
{
    public class UserLoginViewModel
    {
        public bool Active { get; set; } = true;
        [Required]
        [EmailAddress(ErrorMessage = "User name is required")] 
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?#&])[A-Za-z\d$@$!%#*?&]{8,}"
            , ErrorMessage = "Unsupported characters. Please use a combination of numbers, small and capital letters with at least one of the following special characters - $@$!%*#?&")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
    public class UserLoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}