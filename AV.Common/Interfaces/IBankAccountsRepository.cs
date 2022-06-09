using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AV.Common.Entities;

namespace AV.Common.Interfaces
{
    public interface IBankAccountsRepository : IRepository<BankAccount>
    {
        Task<BankAccount> AddBankDetails(Guid accountId, BankAccount bankAccount, CancellationToken cancellationToken);
		Task<Account> AddProfileDetails(Guid accountId, string companyName, ICollection<CompanyLogoDocument> documents, CancellationToken cancellationToken);
	}
}