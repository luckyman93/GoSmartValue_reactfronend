using AutoMapper;
using AV.Common.Interfaces;
using AV.Common.Interfaces.Repositories;
using AV.Contracts.Enums;
using AV.Contracts.Models.Payment.Requests;
using AV.Contracts.Models.Payment.Responses;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AV.Handlers.Payment
{
    public class SetProductAmountRequestHandler : IRequestHandler<SetProductAmountRequest, SetProductAmountResponse>
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IValuationsRepository _valuationsRepository;
        private readonly IMapper _mapper;

        public SetProductAmountRequestHandler(
            IProductsRepository productsRepository,
            IValuationsRepository valuationsRepository,
            IMapper mapper)
        {
            _productsRepository = productsRepository;
            _valuationsRepository = valuationsRepository;
            _mapper = mapper;
        }
        public async Task<SetProductAmountResponse> Handle(SetProductAmountRequest request, CancellationToken cancellationToken)
        {
            await SetProductAmount(request, cancellationToken);
            return _mapper.Map<SetProductAmountResponse>(request);
        }

        private async Task SetProductAmount(SetProductAmountRequest request, CancellationToken cancellationToken)
        {
            // determine pricing rules for the payment type
            // call the pricing service/method for that type => modifies request with correct pricing amount
            switch (request.Type)
            {
                case PaymentType.DetailedReport:
                    await PriceForDetailedReport(request, cancellationToken);
                    break;
                case PaymentType.Other:
                    await PriceForAddCredit(request, cancellationToken);
                    break;
                case PaymentType.Valuation:
                    break;
                case PaymentType.Subscription:
                    break;
                case PaymentType.InstantReport:
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Unable to match a payment type request product.");
            }
        }

        private async Task PriceForAddCredit(SetProductAmountRequest request, CancellationToken cancellationToken)
        {
            var product = await _productsRepository.GetByName(request.Type.ToString(), cancellationToken);
            request.ServiceName = product?.Description ?? "Pre-pay";
            request.ServiceType = product?.ServiceType ?? "38645";
        }

        private async Task PriceForDetailedReport(SetProductAmountRequest request, CancellationToken cancellationToken)
        {
            // get detailed report product for price
            var product = await _productsRepository.GetByName(request.Type.ToString(), cancellationToken);
            var valuation = _valuationsRepository.Get(new Guid(request.Reference));
            // add valuers charge to the product price
            request.Amount = (int)(product.Price + valuation.ServiceFee);
            request.ServiceName = product.Description;
            request.ServiceType = product.ServiceType;
        }
    }
}