using AV.Contracts.Enums;
using AV.Contracts.Models.Basket.Commands;
using FluentValidation;
using System;

namespace AV.Handlers.Basket.Validators;

public interface IConfirmedBasketCommandValidators
{
    IRuleBuilderOptions<CreateBasketTokenCommand, BasketStatus> StatusValidator(
        IRuleBuilderInitial<CreateBasketTokenCommand, BasketStatus> rule);
    IRuleBuilderOptions<CreateBasketTokenCommand, DateTimeOffset?> ConfirmedOnValidator(
        IRuleBuilderInitial<CreateBasketTokenCommand, DateTimeOffset?> rule);
    IRuleBuilderOptions<CreateBasketTokenCommand, CreateBasketTokenCommand> ConfirmedBasketValidator(
        IRuleBuilderInitial<CreateBasketTokenCommand, CreateBasketTokenCommand> rule);
}