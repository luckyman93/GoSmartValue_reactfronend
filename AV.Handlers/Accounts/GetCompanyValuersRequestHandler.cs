using AutoMapper;
using AV.Common.Entities;
using AV.Contracts.Models.Accounts.Commands;
using AV.Contracts.Models.Users;
using AV.Persistence.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AV.Handlers.Accounts
{
    public class GetCompanyValuersRequestHandler : IRequestHandler<GetCompanyValuersRequest, ICollection<ValuerViewModel>>
    {
        private readonly IQueries<Account> _accountsQueries;
        private readonly IMapper _mapper;

        public GetCompanyValuersRequestHandler(IQueries<Account> accountsQueries, IMapper mapper)
        {
            _accountsQueries = accountsQueries;
            _mapper = mapper;
        }

        public async Task<ICollection<ValuerViewModel>> Handle(GetCompanyValuersRequest request, CancellationToken cancellationToken)
        {
            Expression<Func<Account, bool>> companyExp = x => x.Company != null || x.Company == null;
            if (request.Companies.Count > 0)
            {
                companyExp = x => request.Companies.Contains(x.Company.Id);
            }
            var accounts = await _accountsQueries.EntityDbSet
                .Include(a => a.User)
                .Include(a => a.Company)
                .Where(companyExp)
                .Where(a => a.VerifiedByUserId != default
                  && a.IsValuer)
                .ToListAsync(cancellationToken);

            var valuers = accounts.Select(CreateValuerViewModel).ToList();
            return _mapper.Map<ICollection<ValuerViewModel>>(valuers);
        }

        private ValuerViewModel CreateValuerViewModel(Account account)
        {
            return new ValuerViewModel
            {
                FirstName = account.User.FirstName,
                LastName = account.User.LastName,
                AgencyName = account.Company?.Name,
                Id = account.UserId,
                CompanyId = account.Company?.Id,
                LogoUrl = account.Company?.LogoUrl,
            };
        }
    }
}