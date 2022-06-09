using AV.Contracts;
using AV.Contracts.Interface;
using AV.Contracts.Models.Payment.Models;
using AV.Infrastructure.Services.Dpo;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using AV.Contracts.Models.Basket.Commands;
using AV.Contracts.Models.Users;
using IHttpClientFactory = System.Net.Http.IHttpClientFactory;

namespace AV.Infrastructure.Services
{
    /// <summary>
    /// Direct Pay Online services
    /// https://directpayonline.atlassian.net/wiki/spaces/API/overview?homepageId=807369
    /// </summary>
    public class DpoPaymentGateway : IPaymentGatewayService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ConfigurationDpo _dpoConfiguration;
        private readonly ILogger<DpoPaymentGateway> _logger;
        private readonly UserModel _user;

        public DpoPaymentGateway(
            IHttpClientFactory httpClientFactory,
            IOptions<ConfigurationDpo> dpoConfiguration,
            ILogger<DpoPaymentGateway> logger,
            UserModel user)
        {
            _httpClientFactory = httpClientFactory;
            _dpoConfiguration = dpoConfiguration.Value;
            _logger = logger;
            _user = user;
        }
        /*
            option A in the integration process. 
            1. Create Token using the API details. # https://directpayonline.atlassian.net/wiki/spaces/API/pages/36110341/createToken+V6
        Request
Transaction level – Mandatory - Contains all the basic transaction information
Services level – Mandatory - Contains all the information regarding the services sold in the transaction – must contain at-least one service


             Services types:
               3854-Test Product
               5525-Test Service
               https://secure1.sandbox.directpay.online/payv2.php?ID=XXXX Where XXX stands for the token received in the process
             */
        /// <summary>
        /// Creates a DPO Transaction token
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="currency"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        public async Task<CreatePaymentTokenResponse> InitiatePayment(decimal amount, string currency, string returnUrl, string backUrl, string serviceShortName, string paymentReference, string serviceType, UserModel user)
        {
            var request = new Api3G();
            request.CompanyToken = _dpoConfiguration.CompanyToken;
            request.Request = DpoConstant.CreateToken;
            request.Transaction = CreateTransaction(amount, currency, returnUrl, backUrl, paymentReference);
            request.Services = new Dpo.Services
            {
                Service = new Service
                {
                    ServiceDate = DateTime.UtcNow.ToString("yyyy/MM/dd HH:MM"),
                    ServiceDescription = serviceShortName,
                    ServiceType = serviceType
                }
            };

            var response = await PostRequest(request);
            var result = await response.Content.ReadAsStringAsync();
            var dpoResponse = DeserializeObject<Api3G>(result);

            return new CreatePaymentTokenResponse
            {
                ResultCode = dpoResponse.Result,
                ResultExplanation = dpoResponse.ResultExplanation,
                TransRef = dpoResponse.TransRef,
                TransToken = dpoResponse.TransToken
            };
        }

        public Task<string> GeneratePaymentToken(CreateBasketTokenCommand basket)
        {
            throw new NotImplementedException();
        }

        private async Task<HttpResponseMessage> PostRequest(Api3G request)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var httpContent = new StringContent(ToXML(request), Encoding.UTF8, "application/xml");
                var response = await httpClient.PostAsync(_dpoConfiguration.CreateTokenUrl, httpContent);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Unable to create token - invalid call to payment gateway", response);
                }
                Debug.WriteLine($"{response}");
                return response;
            }
        }

        private Transaction CreateTransaction(decimal amount, string currency, string returnUrl, string backUrl, string paymentReference)
        {
            return new Transaction
            {
                BackURL = backUrl,
                CompanyRef = paymentReference,
                CompanyRefUnique = 1,
                PaymentAmount = amount.ToString("#.##"),
                PaymentCurrency = currency,
                RedirectURL = returnUrl
            };
        }
        private T DeserializeObject<T>(string xml)
            where T : new()
        {
            if (string.IsNullOrEmpty(xml))
            {
                return new T();
            }
            try
            {
                using (var stringReader = new StringReader(xml))
                {
                    var serializer = new XmlSerializer(typeof(T));
                    return (T)serializer.Deserialize(stringReader);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"DeserializeObject of string {xml}", ex);
                return new T();
            }
        }

        private static string ToXML(Api3G request)
        {
            using (var stringWriter = new Utf8StringWriter())
            {
                var serializer = new XmlSerializer(request.GetType());
                serializer.Serialize(stringWriter, request);
                var result = stringWriter.ToString();
                Debug.WriteLine(result);
                return result;
            }
        }

        private class DpoConstant
        {
            public const string CreateToken = "createToken";
        }

        public class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;
        }
    }
}
