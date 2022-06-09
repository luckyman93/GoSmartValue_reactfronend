using AV.Contracts.Enums;
using AV.Contracts.Models.Basket.Commands;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using System;

namespace AV.Handlers.Basket.Validators;

public class ConfirmedBasketCommandValidators : IConfirmedBasketCommandValidators
{
    private readonly ISystemClock _systemClock;

    public ConfirmedBasketCommandValidators(ISystemClock systemClock)
    {
        _systemClock = systemClock;
    }

    public IRuleBuilderOptions<CreateBasketTokenCommand, BasketStatus> StatusValidator(IRuleBuilderInitial<CreateBasketTokenCommand, BasketStatus> rule)
    {
        return rule
            .Cascade(CascadeMode.Stop)
            .Must(status => status.Equals(BasketStatus.Confirmed))
                .WithMessage("Basket Must be confirmed.");
    }

    public IRuleBuilderOptions<CreateBasketTokenCommand, DateTimeOffset?> ConfirmedOnValidator(IRuleBuilderInitial<CreateBasketTokenCommand, DateTimeOffset?> rule)
    {
        return rule
            .Cascade(CascadeMode.Stop)
            .Must(x => x.HasValue && x >= _systemClock.UtcNow.AddHours(-24) && x <= _systemClock.UtcNow)
                .WithMessage("Basket must be confirmed within the last 24 hours.");
    }

    public IRuleBuilderOptions<CreateBasketTokenCommand, CreateBasketTokenCommand> ConfirmedBasketValidator(IRuleBuilderInitial<CreateBasketTokenCommand, CreateBasketTokenCommand> rule)
    {
        return rule
            .Cascade(CascadeMode.Stop)
            .Must(x => x.ConfirmedOn.HasValue && x.ConfirmedOn >= _systemClock.UtcNow.AddHours(-24) && x.ConfirmedOn <= _systemClock.UtcNow)
                .WithMessage("Basket must be confirmed within the last 24 hours.")
            .Must(basket => BasketStatus.Confirmed.Equals(basket.Status))
                .WithMessage("Basket Must be confirmed.");
    }
}