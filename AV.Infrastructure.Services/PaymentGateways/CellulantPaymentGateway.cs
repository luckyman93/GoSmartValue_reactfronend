using AV.Common.Entities;
using AV.Common.Interfaces;
using AV.Common.Interfaces.Repositories;
using AV.Contracts.ConfigurationDtos;
using AV.Contracts.Enums;
using AV.Contracts.Interface;
using AV.Contracts.Models;
using AV.Contracts.Models.Basket.Commands;
using AV.Contracts.Models.Payment.Models;
using AV.Infrastructure.Services.PaymentGateways.Cellulant;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using UserModel = AV.Contracts.Models.Users.UserModel;

namespace AV.Infrastructure.Services.PaymentGateways
{
    public class CellulantPaymentGateway : IPaymentGatewayService
    {
        private readonly IOptions<ConfigurationCellulantDto> _cellulantConfig;
        private readonly CellulantEncryptionService _encryptionService;
        private readonly IPaymentsRepository _paymentsRepository;
        private readonly IUserManagerRepository _userManagerRepository;
        private readonly IMapper _mapper;

        public CellulantPaymentGateway(
            IOptions<ConfigurationCellulantDto> cellulantConfig,
            IOptions<CellulantEncryptionService> encryptionService,
            IPaymentsRepository paymentsRepository,
            IUserManagerRepository userManagerRepository,
            IMapper mapper)
        {
            _cellulantConfig = cellulantConfig;
            _encryptionService = encryptionService.Value;
            _paymentsRepository = paymentsRepository;
            _userManagerRepository = userManagerRepository;
            _mapper = mapper;
        }

        public Task<CreatePaymentTokenResponse> InitiatePayment(decimal amount, string currency, string returnUrl, string backUrl, string serviceShortName,
            string paymentReference, string serviceType, UserModel user)
        {
            var payload = new CellulantPaymentDto(_cellulantConfig, amount, paymentReference, user);

            var payloadAsString = JsonSerializer.Serialize(payload);

            return Task.Run(() => new CreatePaymentTokenResponse
            {
                TransRef = paymentReference,
                ResultCode = "200",
                ResultExplanation = "",
                TransToken = _encryptionService.Encrypt(payloadAsString)
            });
        }

        public async Task<string> GeneratePaymentToken(CreateBasketTokenCommand basket)
        {
            var reference = $"{basket.OrderNo}{DateTimeOffset.UtcNow.ToString("yyyymmdd")}";

            var user = await _userManagerRepository.GetAsync(basket.BuyerId);

            var paymentToken = CreatePaymentPayload(basket, reference, _mapper.Map<UserModel>(user));
            // create payment record
            CreatePayment(basket, reference, PaymentType.Other, paymentToken);

            return paymentToken;
        }

        private string CreatePaymentPayload(CreateBasketTokenCommand basket, string paymentReference, UserModel user)
        {
            var payload = new CellulantPaymentDto(_cellulantConfig, basket.GrossTotal, paymentReference, user);
            var payloadAsString = JsonSerializer.Serialize(payload);
            Console.WriteLine("my payload => " + payloadAsString);

            return _encryptionService.Encrypt(payloadAsString);
        }

        private PaymentHistory CreatePayment(CreateBasketTokenCommand basket, string reference, PaymentType productType, string paymentToken)
        {
            var user = _userManagerRepository.GetAll()
                .Include(u => u.Accounts)
                .First(u => u.Id == basket.BuyerId);

            var payment = new PaymentHistory
            {
                AccountId = user.Accounts.First().Id,//user.Accounts.First(u => u.Active).Id
                Reference = reference,
                TransactionReference = reference,
                Amount = basket.GrossTotal,
                Currency = Constants.Currency.Botswana.Code,
                InitiatedById = basket.BuyerId,
                Type = productType,
                BasketId = basket.Id,
                PaymentToken = paymentToken,
                PaymentTokenExpiry = basket.ConfirmedOn.Value.AddHours(24),
                PaymentGateway = "Cellulant",
                Status = PaymentStatus.Pending,
            };

            _paymentsRepository.Add(payment);

            return payment;
        }
    }
}
