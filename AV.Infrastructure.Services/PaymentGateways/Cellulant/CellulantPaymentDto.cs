using AV.Contracts.ConfigurationDtos;
using Microsoft.Extensions.Options;
using System;
using System.Text.Json.Serialization;
using AV.Common.Entities;
using AV.Contracts.Models.Users;

namespace AV.Infrastructure.Services.PaymentGateways.Cellulant
{
    public class CellulantPaymentDto
    {
        public CellulantPaymentDto(IOptions<ConfigurationCellulantDto> cellulantConfig, decimal amount, string paymentReference, UserModel user)
        {
            var cellulantConfig1 = cellulantConfig.Value;
            ServiceCode = cellulantConfig1.ServiceCode;
            CurrencyCode = cellulantConfig1.CurrencyCode;
            Description = cellulantConfig1.ServiceShortName;
            PaymentCompleteApiCallBackUrl = cellulantConfig1.CallBackUrl;
            FailRedirectUrl = cellulantConfig1.FailRedirectUrl;
            SuccessRedirectUrl = cellulantConfig1.SuccessRedirectUrl;
            PendingRedirectUrl = cellulantConfig1.PendingRedirectUrl;

            Amount = amount;
            TransactionId = paymentReference;

            DueDate = DateTime.UtcNow.AddHours(10);
            Description = $"Gosmartvalue.Com product payment. ref#{paymentReference}";

            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            MobileNumber = user.PhoneNumber;
            AccountNumber = user.Id.ToString().Substring(0, 10);
            CountryCode = "BW";
            ClientCode = "";
        }

        // GSV UNIQUE TRANSACTION ID
        [JsonPropertyName("merchantTransactionID")]
        public string TransactionId { get; set; }
        // requestAmount: "100",
        [JsonPropertyName("requestAmount")]
        public decimal Amount { get; set; }
        // currencyCode: "KES",
        [JsonPropertyName("currencyCode")]
        public string CurrencyCode { get; set; }
        // accountNumber: "10092019",
        [JsonPropertyName("accountNumber")]
        public string AccountNumber { get; set; }
        // serviceCode: "<SERVICE_CODE>",
        [JsonPropertyName("serviceCode")]
        public string ServiceCode { get; set; }
        // dueDate: "2019-06-01 23:59:59", //Must be a future date
        [JsonPropertyName("dueDate")] //Must be a future date
        public DateTime DueDate { get; set; }
        // requestDescription: "Dummy merchant transaction",
        [JsonPropertyName("requestDescription")] // e.g Dummy merchant transaction",
        public string Description { get; set; }
        // countryCode: "KE",
        [JsonPropertyName("countryCode")]
        public string CountryCode { get; set; }
        // languageCode: "en",
        [JsonPropertyName("languageCode")]
        public string LanguageCode => "en";
        // payerClientCode: "",
        [JsonPropertyName("payerClientCode")]
        public string ClientCode { get; set; }
        // MSISDN: "+2547XXXXXXXX", //Must be a valid number
        [JsonPropertyName("MSISDN")]
        public string MobileNumber { get; set; }
        // customerFirstName: "John",
        [JsonPropertyName("customerFirstName")]
        public string FirstName { get; set; }
        // customerLastName: "Smith",
        [JsonPropertyName("customerLastName")]
        public string LastName { get; set; }

        // customerEmail: "john.smith@example.com",
        [JsonPropertyName("customerEmail")]
        public string Email { get; set; }

        // successRedirectUrl: "<YOUR_SUCCESS_REDIRECT_URL>",
        /// <summary>
        /// URL where the customer will be redirected to the merchant's site once they complete making full payment for the request 
        /// </summary>
        [JsonPropertyName("successRedirectUrl")]
        public string SuccessRedirectUrl { get; set; }

        // failRedirectUrl: "<YOUR_FAIL_REDIRECT_URL>",
        /// <summary>
        /// URL where the customer will be redirected to if a full payment is not done within the payment window
        /// </summary>
        [JsonPropertyName("failRedirectUrl")]
        public string FailRedirectUrl { get; set; }

        // pendingRedirectUrl: "<YOUR_PENDING_REDIRECT_URL>",
        /// <summary>
        /// The URL that Express checkout will redirect to after a timeout period reaches and the request has not been paid
        /// or when the back button is clicked once you are redirected to checkout. Mandatory is timeout period is configured
        /// </summary>
        [JsonPropertyName("pendingRedirectUrl")]
        public string PendingRedirectUrl { get; set; }

        // paymentWebhookUrl: "<PAYMENT_WEBHOOK_URL>
        /// <summary>
        /// A server(Api) to server(Api) URL Api endpoint that will be called to notify a merchant of a complete payment.
        /// </summary>
        [JsonPropertyName("paymentWebhookUrl")]
        public string PaymentCompleteApiCallBackUrl { get; set; }

    }
}
