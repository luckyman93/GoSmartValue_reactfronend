using AutoMapper;
using AV.Common.Entities;
using AV.Common.Interfaces.Repositories;
using AV.Contracts.Interface;
using AV.Contracts.Models.Basket.Commands;
using AV.Contracts.Models.Payment.Models;
using AV.Persistence.Queries;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AV.Handlers.Basket;

public class GenerateBasketPaymentTokenCommandHandler : IRequestHandler<GenerateBasketPaymentTokenCommand, PaymentTokenDto>
{
    private readonly IMapper _mapper;
    private readonly ILogger<GenerateBasketPaymentTokenCommandHandler> _logger;
    private readonly IBasketsQueries _basketsQueries;
    private readonly IUserManagerRepository _userManagerRepository;
    private readonly IPaymentGatewayService _paymentGatewayService;

    public GenerateBasketPaymentTokenCommandHandler(
        IMapper mapper,
        ILogger<GenerateBasketPaymentTokenCommandHandler> logger,
        IBasketsQueries basketsQueries,
        IUserManagerRepository userManagerRepository,
        IPaymentGatewayService paymentGatewayService)
    {
        _mapper = mapper;
        _logger = logger;
        _basketsQueries = basketsQueries;
        _userManagerRepository = userManagerRepository;
        _paymentGatewayService = paymentGatewayService;
    }

    public async Task<PaymentTokenDto> Handle(GenerateBasketPaymentTokenCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // get user
            var user = _userManagerRepository.Get(request.UserId);
            if (user == null)
            {
                throw new GoSmartValueException($"User with id#:'{request.UserId}' not found.");
            }

            var basket = await _basketsQueries.GetCurrentUserBasketAsync(user.Id, cancellationToken);

            var paymentToken = await _paymentGatewayService.GeneratePaymentToken(_mapper.Map<CreateBasketTokenCommand>(basket));

            if (basket.ConfirmedOn != null)
            {
                return new PaymentTokenDto()
                {
                    BasketId = basket.Id,
                    ExpiryDate = basket.ConfirmedOn.Value.AddHours(24),
                    Token = paymentToken
                };
            }
            throw new GoSmartValueException($"Basket not confirmed.");
        }
        catch (Exception exception)
        {
            _logger.LogError($"Unable to generate payment token.", exception);
            throw new GoSmartValueException(new List<Exception>() { exception });
        }
    }
}