using AutoMapper;
using AV.Common.Entities;
using AV.Common.Interfaces;
using AV.Contracts.Enums;
using AV.Contracts.Models.Payment.Commands;
using AV.Contracts.Models.Payment.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using AV.Common.Interfaces.Repositories;
using AV.Contracts.Interface;

namespace AV.Handlers.Payment
{
    public class SetPaymentToPaidCommandHandler : IRequestHandler<SetPaymentToPaidCommand, PaymentModel>
    {
        private readonly IPaymentsRepository _paymentsRepository;
        private readonly IUserNotificationService _userNotificationService;
        private readonly IUserManagerRepository _userManagerRepository;
        private readonly IComparableRepository _comparableRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private Logger<SetPaymentToPaidCommandHandler> _logger;

        public SetPaymentToPaidCommandHandler(
            IPaymentsRepository paymentsRepository,
            ILoggerFactory factory,
            IUserNotificationService userNotificationService,
            IUserManagerRepository userManagerRepository,
            IComparableRepository comparableRepository,
            IMapper mapper,
            IMediator mediator)
        {
            _logger = new Logger<SetPaymentToPaidCommandHandler>(factory);
            _paymentsRepository = paymentsRepository;
            _userNotificationService = userNotificationService;
            _userManagerRepository = userManagerRepository;
            _comparableRepository = comparableRepository;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<PaymentModel> Handle(SetPaymentToPaidCommand command, CancellationToken cancellationToken)
        {
            var payment = await _paymentsRepository.GetUnpaid(command.TransactionToken);

            if (payment == default)
                throw new Exception($"No unpaid payment record found for '{command.TransactionToken}'");

            UpdatePayment(payment, command);
            if (int.TryParse(command.CCDapproval, out int ccdApproval) && ccdApproval < 1)
            {
                _logger.LogError("Payment failed at merchant.", command);
                payment.Status = PaymentStatus.Declined;
            }

            await _paymentsRepository.SaveUpdatedPayment(payment);

            // Process the product paid for
            if (payment.Status == PaymentStatus.Paid)
            {
                await DeliverPurchasedProduct(payment);
            }
            return _mapper.Map<PaymentModel>(payment);
        }

        private async Task DeliverPurchasedProduct(PaymentHistory payment)
        {
            switch (payment.Type)
            {
                case PaymentType.Subscription:
                    // if account is not active send account confirmation email
                    var userEntity = await _mediator.Send(new RenewAccountSubscriptionCommand(payment.AccountId));
                    //send activation email 
                    await _userNotificationService.SendConfirmationEmail(userEntity.User);
                    // ensure account is active and account is in active state/ expiry date etc
                    break;
                case PaymentType.InstantReport:
                    if (payment.Status == PaymentStatus.Paid)
                    {
                        // Mark Instant Report as paid
                        await _comparableRepository.UpdateComparablePaymentStatus(Guid.Parse(payment.Reference));
                    }
                    break;
                default:
                    break;
            }
        }

        private void UpdatePayment(PaymentHistory payment, SetPaymentToPaidCommand command)
        {
            payment.PnrID = command.PnrID;
            payment.TransID = command.TransID;
            payment.CCDapproval = command.CCDapproval;
            payment.Status = PaymentStatus.Paid;
            payment.UpdatedDate = DateTime.UtcNow;

        }
    }
}