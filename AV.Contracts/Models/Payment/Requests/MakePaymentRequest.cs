using AV.Contracts.Enums;
using AV.Contracts.Models.Payment.Models;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace AV.Contracts.Models.Payment.Requests
{
    public class MakePaymentRequest : IRequest<CreatePaymentTokenResponse>
    {
        [Required]
        public int Amount { get; set; }
        [Required]
        public string Currency { get; set; } = "BWP";
        [Required]
        public PaymentType Type { get; set; }
        public Guid AccountId { get; set; }
        // UserId of person initiating the payment process
        public Guid InitiatedByUserId { get; set; }
        public string ReturnUrl { get; set; }
        public string BackUrl { get; set; }

        public string Reference { get; set; }
        public string ServiceName { get; set; }
        public string ServiceType { get; set; }
    }
}
