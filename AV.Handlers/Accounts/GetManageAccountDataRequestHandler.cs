using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AV.Common.Interfaces.Repositories;
using AV.Contracts.Models.Accounts.Commands;
using AV.Contracts.Models.ViewModels;
using AV.Contracts.Services;
using MediatR;
using Account = AV.Contracts.Models.Account;
using BankAccount = AV.Contracts.Models.BankAccount;

namespace AV.Handlers.Accounts
{
    public class GetManageAccountDataRequestHandler : IRequestHandler<GetManageAccountDataRequest, ManageAccountViewModel>
    {
        private readonly IUserManagerRepository _userManagerRepository;
        private readonly IDocumentStoreService _documentStoreService;
        private readonly IMapper _mapper;

        public GetManageAccountDataRequestHandler(
            IUserManagerRepository userManagerRepository, 
            IDocumentStoreService documentStoreService,
            IMapper mapper)
        {
            _userManagerRepository = userManagerRepository;
            _documentStoreService = documentStoreService;
            _mapper = mapper;
        }

        public async Task<ManageAccountViewModel> Handle(GetManageAccountDataRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManagerRepository.GetAsync(request.UserId);

            var account = user.Accounts
                .FirstOrDefault(ac => ac.AccountType == request.AccountType && ac.Active && ac.ExpiryDate > DateTimeOffset.Now);

            var accountLogos = _documentStoreService.GetDocumentByAccountId(account.Id); 
            
            if (accountLogos.Count() > 0)
            {
                account.CompanyLogos = (ICollection<Common.Entities.CompanyLogoDocument>)accountLogos;
            }
            return new ManageAccountViewModel
            {
                Account = _mapper.Map<Account>(account),
                BankAccounts = _mapper.Map<IEnumerable<BankAccount>>(account?.BankAccounts),
                User = _mapper.Map<Contracts.Models.UserModel>(user)
            };
        }

        
    }
}
