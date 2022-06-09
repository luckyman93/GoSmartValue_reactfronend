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
using AV.Contracts.Models.Basket;

namespace AV.Handlers.Basket;

public class ProcessSuccessFullPaymentCommandHandler : IRequestHandler<ProcessSuccessFullPaymentCommand, CellulantCallBackRequestDTO>
{
    private readonly IMapper _mapper;
    private readonly ILogger<ProcessSuccessFullPaymentCommandHandler> _logger;
    private readonly IBasketsQueries _basketsQueries;
    private readonly IUserManagerRepository _userManagerRepository;
    private readonly IPaymentGatewayService _paymentGatewayService;

    public ProcessSuccessFullPaymentCommandHandler(
        IMapper mapper,
        ILogger<ProcessSuccessFullPaymentCommandHandler> logger,
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


    public async Task<CellulantCallBackRequestDTO> Handle(ProcessSuccessFullPaymentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // get user
            var user = _userManagerRepository.Get(request.UserId);
            if (user == null)
            {
                throw new GoSmartValueException($"User with id#:'{request.UserId}' not found.");
            }

            return null;

            // return new SucceFullPaymentDTO()
            // {
            //     checkoutRequestID = request.SuccessFullPaymentCommand.checkoutRequestID,
            //     merchantTransactionID = request.SuccessFullPaymentCommand.merchantTransactionID,
            //     statusCode = request.SuccessFullPaymentCommand.statusCode,
            //     statusDescription = request.SuccessFullPaymentCommand.statusDescription,
            //     receiptNumber = "GOS-001"
            // };
        }
        catch (Exception exception)
        {
            _logger.LogError($"Unable to generate payment token.", exception);
            throw new GoSmartValueException(new List<Exception>() { exception });
        }
    }
}