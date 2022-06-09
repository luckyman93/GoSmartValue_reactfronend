using System.ComponentModel.DataAnnotations;

namespace GoSmartValue.Web.ViewModels
{
	public class RegisterValuerViewModel:RegisterUserViewModel
	{
		[DataType(DataType.Text)]
		[StringLength(4, MinimumLength = 3)]
		// ReSharper disable once IdentifierTypo
		public string ReibNumber { get; set; }
		[DataType(DataType.Text)]
		[StringLength(11, MinimumLength = 11)]
		[RegularExpression(@"^[R|r][E|e][A|a][C|c]\/[0-9][0-9][0-9]\/[1-1][0-9]$"
			, ErrorMessage = "Please enter valid REAC number. e.g REAC/123/17")]
		// ReSharper disable once IdentifierTypo
		public string ReacNumber { get; set; }
		
		public bool Valuer { get; set; }
		public bool SalesAgent { get; set; }
		public bool RegisteredProfessional { get; set; }
		public string LinkedInUrl { get; set; }
		public string IndustryExperience { get; set; }
		public string CompanyName { get; set; }
	}
}
