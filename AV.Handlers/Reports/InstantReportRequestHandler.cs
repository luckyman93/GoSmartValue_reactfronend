using AutoMapper;
using AV.Common.Entities;
using AV.Common.Interfaces;
using AV.Common.Interfaces.Repositories;
using AV.Contracts;
using AV.Contracts.Enums;
using AV.Contracts.Models.Accounts;
using AV.Contracts.Models.Accounts.Commands;
using AV.Contracts.Models.Payment.Requests;
using AV.Contracts.Models.Reports.Requests;
using AV.Contracts.Models.Valuation;
using AV.Persistence.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AV.Handlers.Reports
{
    public class InstantReportRequestHandler : IRequestHandler<InstantReportRequest, PaymentTrack>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IComparableRepository _comparablesService;
        private readonly IAccountsRepository _accountsRepository;
        private readonly ISubscriptionOptionQueries _subscriptionOptionQuery;
        private readonly ConfigurationDpo _dpoConfigutation;
        private readonly IProductsRepository _productsService;
        private readonly IInstructionRepository _instructionRepository;
        private readonly ILogger<InstantReportRequestHandler> _logger;


        public InstantReportRequestHandler(
            IMapper mapper,
            IMediator mediator,
            IComparableRepository comparablesRepository,
            IAccountsRepository accountsRepository,
            IOptions<ConfigurationDpo> dpoConfiguration,
            ISubscriptionOptionQueries subscriptionOptionQueries,
             IProductsRepository productsService,
             IInstructionRepository instructionRepository,
            ILogger<InstantReportRequestHandler> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _comparablesService = comparablesRepository;
            _accountsRepository = accountsRepository;
            _dpoConfigutation = dpoConfiguration.Value;
            _subscriptionOptionQuery = subscriptionOptionQueries;
            _productsService = productsService;
            _instructionRepository = instructionRepository;
            _logger = logger;
        }
        public async Task<PaymentTrack> Handle(InstantReportRequest request, CancellationToken cancellationToken)
        {
            var reportId = new Guid();
            var instantReportResponse = new PaymentTrack();
            Common.Entities.Product product = null;

            if (request.ReportRequest != null)
            {
                _logger.LogInformation("Request received is for instant report", request);
                product = await _productsService.GetByName("InstantReport", cancellationToken);
                reportId = await SaveComparableRequest(request);
                _logger.LogInformation("instant report request saved.", reportId);
                instantReportResponse = await InitializeResponse(request, reportId, cancellationToken);
            }
            if (request.InstructionRequest != null)
            {
                _logger.LogInformation("Request received is for detailed report", request);

                product = await _productsService.GetByName("DetailedReport", cancellationToken);
                reportId = await SaveInstructionRequest(request);
                instantReportResponse = await InitializeDetailReportResponse(request, reportId, cancellationToken);
                instantReportResponse.AmountToPay = product.Price.Value;
            }
            //set package selected on user account 

            if (instantReportResponse.IsPaying)
            {
                var paymentRequest = await CreatePaymentRequest(request, instantReportResponse, reportId.ToString(), product);

                // initialize payment request with DPO
                var paymentToken = await _mediator.Send(paymentRequest, cancellationToken);
                instantReportResponse.PaymentUrl = $"{_dpoConfigutation.PaymentPageUrl}{paymentToken.TransToken}";
                instantReportResponse.TokenCreated = paymentToken.TransToken != null;

            }
            return instantReportResponse;
        }


        private async Task<PaymentTrack> InitializeResponse(InstantReportRequest request, Guid savedComparableId, CancellationToken cancellationToken)
        {
            _logger.LogInformation("initializing instant report request response..");
            var instantReportResponse = new PaymentTrack();

            if (request.ReportRequest?.SubscriptionOptionId > 0)
            {
                _logger.LogInformation("Getting report request subscriptions..");
                var subscription = await _subscriptionOptionQuery.EntityDbSet
                    .Include(x => x.Package)
                    .ThenInclude(x => x.Products)
                   .FirstOrDefaultAsync(x => x.Id == request.ReportRequest.SubscriptionOptionId);

                instantReportResponse = await SetPackage(request, cancellationToken);
                instantReportResponse.ComparableId = savedComparableId;
                instantReportResponse.IsPaying = true;
                instantReportResponse.PaymentType = PaymentType.Subscription;
                instantReportResponse.AmountToPay = Convert.ToDecimal(subscription.Price);

            }

            else if (request.ReportRequest.SubscriptionOptionId == 0
                     || request.User.Accounts.All(a => a.Active && a.ExpiryDate < DateTime.Now))//adhocpayment
            {
                _logger.LogInformation("Getting report product information..");
                var product = await _productsService.GetByName("InstantReport", cancellationToken);
                instantReportResponse.IsPaying = true;
                instantReportResponse.ComparableId = savedComparableId;
                instantReportResponse.PaymentType = PaymentType.InstantReport;
                instantReportResponse.AmountToPay = product.Price.Value;

            }
            else
            {
                _logger.LogInformation("updating report counts on user subscription..");
                var accountId = request.User.Accounts.First(a => a.Active && a.ExpiryDate > DateTime.Now).Id;
                var exceptionMessage = await _accountsRepository.UpdateReportCounts(accountId, ReportType.InstantReport);
                if (exceptionMessage != null)
                    throw new InvalidOperationException(exceptionMessage);
                else
                    await _comparablesService.UpdateComparablePaymentStatus(savedComparableId);
            }

            return instantReportResponse;
        }

        private async Task<PaymentTrack> InitializeDetailReportResponse(InstantReportRequest request, Guid savedInstructionId, CancellationToken cancellationToken)
        {
            _logger.LogInformation("initializing detailed report request response..");
            var instantReportResponse = new PaymentTrack();

            if (request.InstructionRequest.SubscriptionOptionId > 0)
            {
                _logger.LogInformation("getting users subscription options..");
                var subscription = await _subscriptionOptionQuery.EntityDbSet.Include(x => x.Package).ThenInclude(x => x.Products)
                   .FirstOrDefaultAsync(x => x.Id == request.InstructionRequest.SubscriptionOptionId);

                instantReportResponse = await SetPackage(request, cancellationToken);
                instantReportResponse.InstructionId = savedInstructionId;
                instantReportResponse.IsPaying = true;
                instantReportResponse.PaymentType = PaymentType.Subscription;
                instantReportResponse.AmountToPay = Convert.ToDecimal(subscription.Price);
            }

            else if (request.InstructionRequest.SubscriptionOptionId == 0)//adhocpayment
            {
                _logger.LogInformation("user has no subscription - this therefore an adhoc report request..");
                instantReportResponse.IsPaying = true;
                instantReportResponse.InstructionId = savedInstructionId;
                instantReportResponse.PaymentType = PaymentType.DetailedReport;
            }
            else
            {
                _logger.LogInformation("Report is payable bu users subscription credits..");
                var exceptionMessage = await _accountsRepository.UpdateReportCounts(request.InstructionRequest.CurrentUserAccount.Value,
                    ReportType.DetailedReport);
                if (exceptionMessage != null)
                    throw new InvalidOperationException(exceptionMessage);
                else
                    await _comparablesService.UpdateComparablePaymentStatus(savedInstructionId);
            }

            return instantReportResponse;
        }

        private async Task<Guid> SaveComparableRequest(InstantReportRequest request)
        {
            var comparable = ConvertToComparable(request);
            return await _comparablesService.AddComparable(comparable);
        }

        private async Task<Guid> SaveInstructionRequest(InstantReportRequest request)
        {
            var instruction = _mapper.Map<Instruction>(request.InstructionRequest);
            instruction.IssuerId = request.User.Id;
            return await _instructionRepository.SaveInstruction(instruction);
        }

        private async Task<PaymentTrack> SetPackage(InstantReportRequest request, CancellationToken cancellationToken)
        {
            return await _mediator.Send(
            new SetPackageCommand(request.ReportRequest.SubscriptionOptionId, request.User.Id), cancellationToken);
        }

        private Comparable ConvertToComparable(InstantReportRequest request)
        {
            var initialiseComparable = _mapper.Map<Comparable>(request.ReportRequest);

            initialiseComparable.AddedBy = request.User.Id;
            initialiseComparable.DataState = DataState.Raw;
            initialiseComparable.TransactionType = TransactionType.InstantReport;
            initialiseComparable.LastUpdatedBy = request.User.Id;
            initialiseComparable.PaymentStatus = PaymentStatus.Pending;
            return initialiseComparable;
        }

        private async Task<MakePaymentRequest> CreatePaymentRequest(InstantReportRequest request, PaymentTrack paymentTrack, string reference, Common.Entities.Product product)
        {
            var account = await _accountsRepository.GetUserAccount(request.User.Id);
            return new MakePaymentRequest
            {
                AccountId = account.Id,
                Amount = Convert.ToInt32(paymentTrack.AmountToPay),
                BackUrl = CreateBackUrl(reference),
                Currency = "BWP",
                InitiatedByUserId = request.User.Id,
                Reference = reference,
                ReturnUrl = $"{Environment.GetEnvironmentVariable("Hostname")}/payment/paymentSuccess?accountId={paymentTrack.AccountId}&comparableId={paymentTrack.ComparableId}&instructionId={paymentTrack.InstructionId}&",
                ServiceName = product.Name,
                ServiceType = product.ServiceType
            };
        }

        private static string CreateBackUrl(string reference)
        {
            return $"{Environment.GetEnvironmentVariable("Hostname")}/payment/payment?reference={reference}";
        }
    }
}
