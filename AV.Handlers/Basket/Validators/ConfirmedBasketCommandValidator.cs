using AV.Contracts.Models.Basket.Commands;
using FluentValidation;

namespace AV.Handlers.Basket.Validators;

internal class ConfirmedBasketCommandValidator : AbstractValidator<CreateBasketTokenCommand>
{
    public ConfirmedBasketCommandValidator(IConfirmedBasketCommandValidators confirmedBasketCommandValidators)
    {
        CascadeMode = CascadeMode.Stop;
        confirmedBasketCommandValidators.ConfirmedOnValidator(RuleFor(request => request.ConfirmedOn));
        confirmedBasketCommandValidators.StatusValidator(RuleFor(request => request.Status));
    }
}