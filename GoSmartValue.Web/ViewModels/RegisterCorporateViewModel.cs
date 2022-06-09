using System.ComponentModel.DataAnnotations;

namespace GoSmartValue.Web.ViewModels
{
    public class RegisterCorporateViewModel : RegisterUserViewModel
    {
        public bool Corporate { get; set; }
        public bool Banker { get; set; }
        public bool Insurance { get; set; }
        public bool GovernmentAgency { get; set; }
        public bool Developer { get; set; }

        [Required]
        public string CompanyName { get; set; }
    }
}