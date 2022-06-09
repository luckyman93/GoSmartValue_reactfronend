using AV.Contracts.Enums;
using System;

namespace AV.Contracts.Models.Accounts
{
    public class PaymentDetail
    {

        public PaymentDetail(decimal amount,
                string currency,
                PaymentType paymentType,
                PaymentStatus paymentStatus,
                string reference,
                string transactionReference,
                Guid accountId,
                Guid initiatedById,
                string paymentUrl)
            {
                Amount = amount;
                Currency = currency;
                PaymentType = paymentType;
                PaymentStatus = paymentStatus;
                Reference = reference;
                TransactionReference = transactionReference;
                AccountId = accountId;
                InitiatedById = initiatedById;
                PaymentUrl = paymentUrl;
            }


            public Guid Id { get; set; }
            public decimal Amount { get; set; }
            public string Currency { get; set; }
            public PaymentType PaymentType { get; }
            public PaymentStatus PaymentStatus { get; }
            public PaymentType Type { get; set; }
            public PaymentStatus Status { get; set; }
            public string Reference { get; set; }
            public string TransactionReference { get; set; }
            public Guid AccountId { get; set; }
            public Guid InitiatedById { get; set; }
            public string PaymentUrl { get; set; }
        
    }
}
