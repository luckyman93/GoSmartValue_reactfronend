using AV.Contracts.Models.Basket.Commands;
using AV.Contracts.Models.Payment.Models;
using System.Threading.Tasks;
using AV.Contracts.Models.Users;

namespace AV.Contracts.Interface
{
    public interface IPaymentGatewayService
    {
        Task<CreatePaymentTokenResponse> InitiatePayment(decimal amount, string currency, string returnUrl, string backUrl, string serviceShortName, string paymentReference, string serviceType, UserModel user = null);
        Task<string> GeneratePaymentToken(CreateBasketTokenCommand basket);
    }
}
