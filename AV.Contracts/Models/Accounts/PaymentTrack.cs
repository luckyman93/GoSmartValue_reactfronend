using AV.Contracts.Enums;
using System;

namespace AV.Contracts.Models.Accounts
{
    public class PaymentTrack
    {
        public Guid? AccountId { get; set; }
        public Guid? InstructionId { get; set; }
        public Guid? ComparableId { get; set; }
        public bool IsPaying { get; set; }
        public decimal AmountToPay { get; set; }
        public string PaymentUrl { get; set; }
        public bool TokenCreated { get; set; }
        public PaymentDetail PaymentDetail { get; set; }
        public PaymentType? PaymentType { get; set; }
    }
}
