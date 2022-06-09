using AutoMapper;
using AV.Contracts.Models.Basket;
using AV.Contracts.Models.Basket.Commands;
using AV.Persistence.Queries;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using System.Threading;

namespace AV.Handlers.Basket.Validators;

public class GenerateBasketPaymentTokenCommandValidator : AbstractValidator<GenerateBasketPaymentTokenCommand>
{
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IBasketsQueries _basketsQueries;

    public GenerateBasketPaymentTokenCommandValidator(
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        IBasketsQueries basketsQueries,
        IConfirmedBasketCommandValidators confirmedBasketCommandValidators)
    {
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _basketsQueries = basketsQueries;

        RuleFor(x => GetCurrentUserBasket(x))
            .SetValidator(new ConfirmedBasketCommandValidator(confirmedBasketCommandValidators));

        // validate basket
        //new ConfirmedBasketCommandValidator(confirmedBasketCommandValidators)
        //    .Validate(basket);
    }

    private CreateBasketTokenCommand GetCurrentUserBasket(GenerateBasketPaymentTokenCommand generateBasketPaymentTokenCommand)
    {
        // get basket
        var userId = GetLoggedInUserIdAsync();
        return _mapper.Map<CreateBasketTokenCommand>(_basketsQueries.GetOrCreateCurrentUserBasket(userId, CancellationToken.None));
    }

    private Guid GetLoggedInUserIdAsync()
    {
        var userId = _httpContextAccessor.HttpContext
            .User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;
        return Guid.Parse(userId);
    }
}