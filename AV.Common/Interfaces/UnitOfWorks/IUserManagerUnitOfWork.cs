using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AV.Common.Entities;
using AV.Contracts.Enums;

namespace AV.Common.Interfaces.UnitOfWorks
{
    public interface IUserManagerUnitOfWork : IUnitOfWork
    {
        IList<User> GetAllUsers(bool activeOnly = false);
        User GetUser(Guid userId);
        void SavePasswordResetLink(Guid userId, string passwordResetLink);
        IList<Account> GetUserAccounts();
        bool HasActiveAccount(string userName);
        ICollection<Account> GetUserAccounts(Guid userId);
        Task<User> GetUserByUserName(string userName);
        void CreateAccountRecord(Guid userId, SubscriptionType subscriptionType, bool isValuer, bool isSalesAgent, bool isCorporate, bool isBanker, bool isInsurance, bool isGovernmentAgency, bool isDeveloper, string companyName);
        User GetUserWithRoles(Guid id);
        void ResetReturnUrl(User user);
    }
}