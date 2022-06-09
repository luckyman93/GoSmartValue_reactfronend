using AV.Contracts.Enums;
using AV.Contracts.Models.Payment.Responses;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace AV.Contracts.Models.Payment.Requests
{
    public class SetProductAmountRequest : IRequest<SetProductAmountResponse>
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
        [Required]
        public string Reference { get; set; }
        public string ServiceName { get; set; }
        public string ServiceType { get; set; }
    }
}