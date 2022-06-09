using AV.Common.Entities;
using AV.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AV.Persistence.EntityFramework.Repositories
{
    public class BankAccountsRepository : Repository<BankAccount>, IBankAccountsRepository
    {
        public BankAccountsRepository(ValuationsContext context) : base(context)
        { }

        public async Task<BankAccount> AddBankDetails(Guid accountId, BankAccount bankDetails,
            CancellationToken cancellationToken)
        {
            bankDetails.AccountId = accountId;
            await DbContext.Set<BankAccount>().AddAsync(bankDetails, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
            await DbContext.Entry(bankDetails).ReloadAsync(cancellationToken);
            return bankDetails;
        }

        public async Task<Account> AddProfileDetails(Guid accountId, string companyName, ICollection<CompanyLogoDocument> documents, CancellationToken cancellationToken)
        {
            var account = await DbContext.Set<Account>().FirstOrDefaultAsync(a => a.Id == accountId, cancellationToken);
            if (!string.IsNullOrEmpty(companyName))
            {
                account.CompanyName = companyName;
            }

            foreach (var document in documents)
            {
                account.CompanyLogos.Add(document);
            }

            await DbContext.SaveChangesAsync(cancellationToken);
            await DbContext.Entry(account).ReloadAsync(cancellationToken);
            return account;
        }
    }
}