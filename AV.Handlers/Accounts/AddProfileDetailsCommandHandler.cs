using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AV.Common.Interfaces;
using AV.Contracts.Models.Accounts.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AV.Common.Entities;
using Account = AV.Contracts.Models.Account;
using AV.Contracts.Services;
using AV.Contracts.Models;

namespace AV.Handlers.Accounts
{
    public class AddProfileDetailsCommandHandler : IRequestHandler<AddProfileDetailsCommand, Account>
	{
        private readonly IDocumentStoreService _documentStore;
        private readonly IMapper _mapper;
        public IBankAccountsRepository BankAccountsRepository { get; }

        public AddProfileDetailsCommandHandler(
            IBankAccountsRepository bankAccountsRepository, 
            IDocumentStoreService documentStore,
            IMapper mapper)
        {
            _documentStore = documentStore;
            _mapper = mapper;
            BankAccountsRepository = bankAccountsRepository;
        }

        public async Task<Account> Handle(AddProfileDetailsCommand command, CancellationToken cancellationToken)
        {
            // if there are logos then select first upload to document store and save documentId on account
            var documents = new List<CompanyLogoDocument>();
            if (command.LogoDocument.Any())
            {
                documents = (List<CompanyLogoDocument>)_documentStore.ConvertToInstructionDocument<CompanyLogoDocumentModel>(command.LogoDocument);
            }

            var profileDetails = await BankAccountsRepository.AddProfileDetails(command.AccountId, command.CompanyName, documents, cancellationToken);
            return profileDetails != null
                ? _mapper.Map<Account>(profileDetails)
                : default;
        }

        
    }
}
