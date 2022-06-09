using System;
using AV.Common.Entities;
using AV.Common.Interfaces;
using AV.Contracts.Enums;
using AV.Contracts.Interface;
using AV.Contracts.Models.Payment.Models;
using AV.Contracts.Models.Payment.Requests;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AV.Handlers.Payment
{
    public class MakePaymentRequestHandler : IRequestHandler<MakePaymentRequest, CreatePaymentTokenResponse>
    {
        private readonly IPaymentGatewayService _paymentService;
        private readonly IPaymentsRepository _paymentsRepository;

        public MakePaymentRequestHandler(IPaymentGatewayService paymentService, IPaymentsRepository paymentsRepository)
        {
            _paymentService = paymentService;
            _paymentsRepository = paymentsRepository;
        }
        public async Task<CreatePaymentTokenResponse> Handle(MakePaymentRequest request, CancellationToken cancellationToken)
        {

            // create a payment record - for our reference - initially pending
            var payment = CreatePayment(request, Guid.NewGuid().ToString(), null);
            await _paymentsRepository.AddPayment(payment);

            //request.Reference = payment.Reference;
            // make payment request to payment gateway
            var paymentResponse = await _paymentService.InitiatePayment(request.Amount, request.Currency, request.ReturnUrl, request.BackUrl, request.ServiceName, request.Reference, request.ServiceType);
            
            payment.TransID = string.IsNullOrEmpty(paymentResponse.TransRef) ? payment.TransID : paymentResponse.TransRef;
            payment.TransactionToken = paymentResponse.TransToken;

            await _paymentsRepository.UpdateAsync(payment);

            return paymentResponse;
        }

        private PaymentHistory CreatePayment(MakePaymentRequest request, string transactionReference, string transactionToken)
        {
            return new PaymentHistory
            {
                AccountId = request.AccountId,
                Reference = request.Reference,
                TransactionReference = transactionReference,
                TransactionToken = transactionToken,
                Amount = request.Amount,
                Currency = request.Currency,
                InitiatedById = request.InitiatedByUserId,
                Status = PaymentStatus.Pending,
                Type = request.Type
            };
        }
    }
}
