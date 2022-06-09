using AV.Contracts.Enums;
using System;

namespace AV.Contracts.Models.Payment.Models
{
    public class PaymentModel
    {
        public DateTime UpdatedDate { get; set; }
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public PaymentType Type { get; set; }
        public PaymentStatus Status { get; set; }
        public string Reference { get; set; }
        public string TransactionToken { get; set; }
        public string TransactionReference { get; set; }
        public Guid AccountId { get; set; }
        public Guid InitiatedById { get; set; }
        public string CompanyRef { get; set; }
        public string PnrID { get; set; }
        public string TransID { get; set; }
        public string CCDapproval { get; set; }
    }
}
