using AV.Common.Entities;
using AV.Common.Interfaces;
using AV.Contracts.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AV.Persistence.EntityFramework.Repositories
{
    public class PaymentsRepository : Repository<PaymentHistory>, IPaymentsRepository
    {
        private readonly IProductsRepository _product;
        public PaymentsRepository(ValuationsContext context, IProductsRepository product) : base(context)
        {
            _product = product;
        }

        public async Task AddPayment(PaymentHistory payment)
        {
            var existingPayment = await DbContext.Set<PaymentHistory>()
                .FirstOrDefaultAsync(p =>
                    p.Reference == payment.Reference
                    && p.Type == payment.Type
                    && p.Amount == payment.Amount);

            if (existingPayment != default)
            {
                if (existingPayment.Status == PaymentStatus.Paid)
                {
                    throw new InvalidOperationException($"A payment has already been received for: #Reference:{payment.Reference} #Type:{payment.Type} #Type:{payment.Amount}");
                }
                existingPayment.TransactionReference = payment.TransactionReference;
                existingPayment.TransactionToken = payment.TransactionToken;
                existingPayment.Status = payment.Status;
            }
            else
            {
                DbContext.Set<PaymentHistory>().Add(payment);
            }
            await SaveChangesAsync();
            DbContext.Entry(payment).Reload();
        }

        public async Task<User> RenewAccountSubscription(Guid accountId)
        {
            // fetch all record matching reference and type
            var existingAccount = await DbContext.Set<Account>()
                .Include(u => u.User)
                .FirstOrDefaultAsync(ac =>
                    ac.Id == accountId)
                ;

            if (existingAccount == null)
            {
                throw new ArgumentNullException($"No Account found for: #Account:{accountId}");

            };

            existingAccount.User.Active = true;
            existingAccount.ExpiryDate = DateTime.Now.AddYears(1);
            existingAccount.User.EmailConfirmed = true;

            DbContext.Entry(existingAccount).State = EntityState.Modified;
            await SaveChangesAsync();
            DbContext.Entry(existingAccount).Reload();
            return existingAccount.User;
        }

        public async Task<string> ActivateStandardUserSubscription(Guid accountId)
        {
            var account = await DbContext.Set<Account>().FindAsync(accountId);
            if (account == null)
                return "Account not found";

            if (account.AccountType != AccountType.Standard)
                throw new InvalidOperationException($"Invalid account type. Account must be for Standard user.");

            if (account.SubscriptionOptionId == null)
                return "No subscription set";

            //get package information 
            var option = await DbContext.Set<SubscriptionOption>().Include(p => p.Package)
                .ThenInclude(x => x.Products)
                .FirstOrDefaultAsync(x => x.Id == account.SubscriptionOptionId);
            if (option == null)
                return "Package not found";

            var packageProducts = DbContext.Set<Product>().Where(x => option.Package.Products.Contains(x));

            //account.DetailedReportsLimit = await packageProducts.SumAsync(x => x.DetailedReportsLimit);
            //account.InstantReportsLimit = await packageProducts.SumAsync(x => x.InstantReportsLimit);
            account.ExpiryDate = GetExpiryDate(option.Frequency);
            account.DiscountPerReferral = option.Package.DiscountPerReferral;
            account.Frequency = option.Frequency;
            account.MaximumSubAccounts = option.Package.MaximumSubAccounts;
            account.Status = SubscriptionStatus.Active;


            try
            {
                await DbContext.SaveChangesAsync();
                return "saved";
            }
            catch
            {
                return "failed to update the account";
            }

        }

        public async Task<bool> HasTransactionRef(string paymentRef)
        {
            return await DbContext.Set<PaymentHistory>()
                .CountAsync(x => x.TransactionReference == paymentRef) > 0;
        }

        public async Task SaveUpdatedPayment(PaymentHistory payment)
        {
            DbContext.Entry(payment).State = EntityState.Modified;
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(PaymentHistory payment)
        {
            await SaveUpdatedPayment(payment);
        }

        public async Task<PaymentHistory> GetUnpaid(string token)
        {
            return await DbContext.Set<PaymentHistory>()
                .FirstOrDefaultAsync(p =>
                    p.TransactionToken.Trim().ToLower() == token.Trim().ToLower()
                    && p.Status != PaymentStatus.Paid);
        }

        private DateTime GetExpiryDate(PaymentFrequency frequency)
        {
            PaymentFrequency paymentFrequency = frequency;
            DateTime date = DateTime.Now;
            switch (paymentFrequency)
            {
                case PaymentFrequency.Monthly:
                    return date.AddMonths(1);
                case PaymentFrequency.Quarterly:
                    return date.AddMonths(3);
                case PaymentFrequency.Yearly:
                    return date.AddYears(1);
                default:
                    return date;

            }
        }
    }
}