using System;
using MediatR;

namespace AV.Contracts.Models.Accounts.Commands
{
    public class AddBankDetailsCommand : IRequest<BankAccount>
    {
        public Guid AccountId { get; set; }
        public Guid UserId { get; set; }
        public BankAccount BankAccount { get; set; }
    }
}
