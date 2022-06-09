namespace AV.Contracts.Models.Payment.Models
{
    public class CreatePaymentTokenResponse
    {
        public string ResultCode { get; set; }
        public string ResultExplanation { get; set; }
        public string TransRef { get; set; }
        public string TransToken { get; set; }
    }
}
