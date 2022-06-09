using AV.Common.Interfaces;
using AV.Contracts.Models.Accounts;
using AV.Contracts.Models.Accounts.Commands;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AV.Handlers.Accounts
{
    public class SetPackageCommandHandler : IRequestHandler<SetPackageCommand, PaymentTrack>
    {
        private readonly IAccountsRepository _account;
        public SetPackageCommandHandler(IAccountsRepository account)
        {
            _account = account;
        }
        public async Task<PaymentTrack> Handle(SetPackageCommand request, CancellationToken cancellationToken)
        {
            var account = _account.GetAll().FirstOrDefault(x => x.UserId == request.UserId && x.ExpiryDate > DateTime.UtcNow);
            if (account == null)
                return null;
            account.SubscriptionOptionId = request.SubscriptionOptionId;
            if (!await _account.SetPackage(account))
            {
                return null;
            }
            return new PaymentTrack() { AccountId = account.Id };
        }
    }
}
