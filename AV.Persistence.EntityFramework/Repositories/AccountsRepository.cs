using AV.Common.Entities;
using AV.Common.Interfaces;
using AV.Contracts.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AV.Persistence.EntityFramework.Repositories
{
    public class AccountsRepository : Repository<Account>, IAccountsRepository
    {
        private readonly ILogger<AccountsRepository> _logger;

        public AccountsRepository(ValuationsContext context,
            ILogger<AccountsRepository> logger) : base(context)
        {
            _logger = logger;
        }

        public async Task<Account> GetUserAccount(Guid userId)
        {
            return await DbContext.Set<Account>().FirstOrDefaultAsync(ac => ac.UserId == userId);
        }

        public async Task<bool> SetPackage(Account account)
        {
            try
            {
                //get account to update
                var updateAccount = await DbContext.Set<Account>().FindAsync(account.Id);
                //get subscription
                var subscription = await DbContext.Set<SubscriptionOption>().FindAsync(account.SubscriptionOptionId);
                updateAccount.Status = SubscriptionStatus.PaymentPending;
                updateAccount.SubscriptionOptionId = account.SubscriptionOptionId;
                updateAccount.Cost = subscription.Price;
                updateAccount.Frequency = subscription.Frequency;

                DbContext.Set<Account>().Update(updateAccount);
                await DbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error whilst trying to set package on account.", ex);
                return false;
            }
        }
        public async Task<string> UpdateReportCounts(Guid accountId, ReportType reportType)
        {
            var account = await DbContext.Set<Account>().FindAsync(accountId);

            if (reportType == ReportType.DetailedReport)
                if (account.DetailedReportsLimit < 1)
                    return "detailed report limit reached";
                else
                    account.DetailedReportsLimit--;

            else if (reportType == ReportType.InstantReport)
                if (account.InstantReportsLimit < 1)
                    return "Instant report limit reached";
                else
                    account.InstantReportsLimit--;
            return null;
        }
    }
}