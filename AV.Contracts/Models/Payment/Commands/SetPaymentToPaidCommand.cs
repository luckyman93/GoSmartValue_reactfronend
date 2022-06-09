using AV.Contracts.Enums;
using AV.Contracts.Models.Payment.Models;
using MediatR;
using System;

namespace AV.Contracts.Models.Payment.Commands
{
    public class SetPaymentToPaidCommand : IRequest<PaymentModel>
    {
        public PaymentType Type { get; set; }
        public string Reference { get; set; }
        public string TransID { get; set; }
        public string CCDapproval { get; set; }
        public string PnrID { get; set; }
        public string TransactionToken { get; set; }
        public string CompanyRef { get; set; }
        public Guid? AccountId { get; set; }
        public Guid? ComparableId { get; set; }
        public Guid? InstructionId { get; set; }

    }
}
