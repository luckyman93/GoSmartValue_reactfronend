using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AV.Common.Interfaces;
using AV.Contracts.Models;
using AV.Contracts.Models.Accounts.Commands;
using MediatR;

namespace AV.Handlers.Accounts
{
    public class AddBankDetailsCommandHandler : IRequestHandler<AddBankDetailsCommand, BankAccount>
    {
        private readonly IMapper _mapper;
        public IBankAccountsRepository BankAccountsRepository { get; }

        public AddBankDetailsCommandHandler(IBankAccountsRepository bankAccountsRepository, IMapper mapper)
        {
            _mapper = mapper;
            BankAccountsRepository = bankAccountsRepository;
        }

        public async Task<BankAccount> Handle(AddBankDetailsCommand command, CancellationToken cancellationToken)
        {
            var bankDetails = await BankAccountsRepository
                .AddBankDetails(command.AccountId, _mapper.Map<Common.Entities.BankAccount>(command.BankAccount), cancellationToken);
            return bankDetails != null
                ? _mapper.Map<BankAccount>(bankDetails)
                : default;
        }
    }
}
