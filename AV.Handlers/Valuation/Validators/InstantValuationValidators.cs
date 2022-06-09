using AV.Contracts.Models.Valuation;
using FluentValidation;


namespace AV.Handlers.Valuation.Validators
{
	public class InstantValuationValidators : AbstractValidator<ComparableRequestViewModel>
	{
		public InstantValuationValidators()
		{
			RuleFor(x => x.LocationName).NotEmpty().WithMessage("Location is required.");
			RuleFor(x => x.PlotNo).NotEmpty().WithMessage("Plot number is required.");
			RuleFor(x => x.Size).NotEmpty().WithMessage("Plot size is required.");
			RuleFor(x => x.LandUse).NotEmpty().WithMessage("Land use is required.");
			RuleFor(x => x.PropertyType).NotEmpty().WithMessage("Property type is required.");
		}
	}
}
