using AV.Common.Entities;
using AV.Contracts.Enums;
using System;
using System.Threading.Tasks;

namespace AV.Common.Interfaces
{
    public interface IAccountsRepository : IRepository<Account>
    {
        Task<Account> GetUserAccount(Guid userId);
        Task<bool> SetPackage(Account account);
        Task<string> UpdateReportCounts(Guid accountId, ReportType reportType);
    }
}