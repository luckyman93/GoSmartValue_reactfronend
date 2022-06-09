using AutoMapper;
using AV.Common.Entities;
using AV.Common.Interfaces.Repositories;
using AV.Common.Interfaces.Services;
using AV.Contracts.Enums;
using AV.Contracts.Models.Basket;
using AV.Contracts.Models.Basket.Commands;
using AV.Persistence.Queries;
using AV.Persistence.Stores;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AV.Handlers.Basket;

public class ConfirmBasketCommandHandler : IRequestHandler<ConfirmBasketCommand, BasketDto>
{
    private readonly IMapper _mapper;
    private readonly ILogger<ConfirmBasketCommandHandler> _logger;
    private readonly IBasketsQueries _basketsQueries;
    private readonly IOrderNumberService _orderNumberService;
    private readonly IStore<Common.Entities.Basket> _basketsStore;
    private readonly IUserManagerRepository _userManagerRepository;

    public ConfirmBasketCommandHandler(
        IMapper mapper,
        ILogger<ConfirmBasketCommandHandler> logger,
        IBasketsQueries basketsQueries,
        IOrderNumberService orderNumberService,
        IStore<Common.Entities.Basket> basketsStore,
        IUserManagerRepository userManagerRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _basketsQueries = basketsQueries;
        _orderNumberService = orderNumberService;
        _basketsStore = basketsStore;
        _userManagerRepository = userManagerRepository;
    }

    public async Task<BasketDto> Handle(ConfirmBasketCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // get user
            var user = _userManagerRepository.Get(request.UserId); if (user == null)
            {
                throw new GoSmartValueException($"User with id#:'{request.UserId}' not found.");
            }

            // Initialize current user basket
            var usersCurrentBasket = _basketsQueries.Find(b => b.BuyerId == request.UserId 
                                                               && b.Status.Equals(BasketStatus.Draft)
                                                               || b.Status.Equals(BasketStatus.Confirmed));
            if (usersCurrentBasket == null)
                throw new GoSmartValueException($"No Active basket found for user.");

            if (string.IsNullOrEmpty(usersCurrentBasket.OrderNo))
                usersCurrentBasket.OrderNo = await _orderNumberService.GenerateNextOrderNo();
            else
                usersCurrentBasket.OrderNo = usersCurrentBasket.OrderNo;

            usersCurrentBasket.Status = BasketStatus.Confirmed;
            usersCurrentBasket.ConfirmedOn = DateTimeOffset.UtcNow;

            _logger.LogInformation($"User#{user.Id} confirmed basket#{usersCurrentBasket.Id} on {usersCurrentBasket.ConfirmedOn}.");

            await _basketsStore.Update(usersCurrentBasket, cancellationToken);

            return _mapper.Map<BasketDto>(usersCurrentBasket);
        }
        catch (Exception exception)
        {
            _logger.LogError($"Unable to confirm basket.", exception);
            throw;
        }
    }
}