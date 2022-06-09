using AutoMapper;
using AV.Common.Entities;
using AV.Contracts.Enums;
using AV.Contracts.Models.Accounts;
using AV.Contracts.Models.Accounts.Commands;
using AV.Persistence.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AV.Handlers.Accounts
{
    public class GetUserActiveSubscriptionRequestHandler : IRequestHandler<GetUserSubscriptionRequest, UserActiveSubscriptionViewModel>
    {
        private readonly IQueries<Account> _accountsQueries;
        private readonly IMapper _map;
        public GetUserActiveSubscriptionRequestHandler(IQueries<Account> accountsQueries, IMapper map)
        {
            _accountsQueries = accountsQueries;
            _map = map;
        }
        public async Task<UserActiveSubscriptionViewModel> Handle(GetUserSubscriptionRequest request, CancellationToken cancellationToken)
        {
            return await _accountsQueries.EntityDbSet
                .Include(sub => sub.SubscriptionOption)
                .Where(acc => acc.UserId == request.UserId && acc.SubscriptionType == SubscriptionType.Standard
                        && acc.Status == SubscriptionStatus.Active)

                .Select(sub => _map.Map<Account, UserActiveSubscriptionViewModel>(sub))
                .FirstOrDefaultAsync();
        }
    }
}
