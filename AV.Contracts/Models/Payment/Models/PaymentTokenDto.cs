using System;

namespace AV.Contracts.Models.Payment.Models
{
    public class PaymentTokenDto
    {
        public int BasketId { get; set; }
        public DateTimeOffset ExpiryDate { get; set; }
        public string Token { get; set; }
    }
}
