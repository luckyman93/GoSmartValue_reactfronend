using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AV.Common.Entities;
using AV.Common.Interfaces.Repositories;
using AV.Common.Interfaces.UnitOfWorks;
using AV.Contracts.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AV.Persistence.EntityFramework.UnitOfWorks
{
    public class UserManagerUnitOfWork : UnitOfWork, IUserManagerUnitOfWork
    {
        private readonly IUserManagerRepository _userManagerRepository;
        private readonly Logger<UserManagerUnitOfWork> _logger;

        public UserManagerUnitOfWork(IdentityDbContext<User, Role, Guid> dbContext, IUserManagerRepository userManagerRepository)
            : base(dbContext)
        {
            _logger = new Logger<UserManagerUnitOfWork>(new LoggerFactory());
            _userManagerRepository = userManagerRepository;
        }

        public IList<User> GetAllUsers(bool activeOnly = false)
        {
            return activeOnly
                ? _userManagerRepository.GetAll()
                    .Include(u => u.Roles)
                    .Include(u => u.Accounts)
                    .Where(u => u.Active).ToList()
                : _userManagerRepository.GetAll().ToList();
        }

        public User GetUser(Guid userId)
        {
            return _userManagerRepository.Get(userId);
        }

        public User GetUserWithRoles(Guid userId)
        {
            return _userManagerRepository.Find(u => u.Id == userId)
                .Include( u => u.Roles)
                .SingleOrDefault();
        }

        public User GetUserWithAccounts(Guid userId)
        {
            return _userManagerRepository.Find(u => u.Id == userId)
                .Include(u => u.Accounts)
                .SingleOrDefault();
        }

        public void ResetReturnUrl(User user)
        {
            user.ReturnUrl = string.Empty;
            Complete();
        }


        public Task<User> GetUserByUserName(string userName)
        {
            return _userManagerRepository
                .Find(u => u.UserName.Equals(userName.Trim(), StringComparison.CurrentCultureIgnoreCase))
                .SingleOrDefaultAsync();
        }

        public void CreateAccountRecord(Guid userId, SubscriptionType subscriptionType, bool isValuer, bool isSalesAgent, bool isCorporate, bool isBanker, bool isInsurance, bool isGovernmentAgency, bool isDeveloper, string companyName)
        {
            //return if user is not active or has an active account already has an account
            var currentUser = _userManagerRepository.Get(userId);
            if (!currentUser.Active || (currentUser.Accounts != null && currentUser.Accounts.Any(ac => ac.Active)))
                return;
            var newAccount = GetNewAccount(currentUser, isValuer,
	            isSalesAgent,
	            isCorporate,
	            isBanker,
	            isInsurance,
	            isGovernmentAgency,
	            isDeveloper,
	            companyName,
                subscriptionType);
            if (currentUser.Accounts == null || !currentUser.Accounts.Any())
            {
                newAccount.User = currentUser;
                _dbContext.Set<Account>().Update(newAccount);
            }
            else
            {
                //check that no matching active account record exists for user
                var account = currentUser.Accounts.SingleOrDefault(ac =>
                    ac.Active && ac.ExpiryDate > DateTime.UtcNow && ac.AccountType == newAccount.AccountType);
                if (account == null)
                {
                    newAccount.User = currentUser;
                }
                else
                {
                    // If matched record found then update is
                    UpdateAccount(account, newAccount);
                }
                _dbContext.Set<Account>().Update(newAccount);
            }
            Complete();
        }

        private void UpdateAccount(Account account, Account newAccount)
        {
            account.ExpiryDate = newAccount.ExpiryDate;
            account.IsCorporate = newAccount.IsCorporate;
            account.IsValuer = newAccount.IsValuer;
            account.IsSalesAgent = newAccount.IsSalesAgent;
            account.SubscriptionType = newAccount.SubscriptionType;
        }

        private Account GetNewAccount(User user, bool isValuer, bool isSalesAgent, bool isCorporate, bool isBanker, bool isInsurance, bool isGovernmentAgency, bool isDeveloper, string companyName, SubscriptionType subscriptionType)
        {
            return new Account
            {
                AccountType = ResolveAccountType(isValuer, isSalesAgent,isCorporate),
                AddedOn = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(60),
                IsValuer = isValuer,
                IsSalesAgent = isSalesAgent,
                IsCorporate = isCorporate,
                IsBanker = isBanker,
                IsInsurance = isInsurance,
                IsGovernmentAgency = isGovernmentAgency,
                IsDeveloper = isDeveloper,
                CompanyName = companyName,
                VerifiedByUserId = user.Id,
                //Primary User
                User = user,
                SubscriptionType = subscriptionType
            };
        }

        private AccountType ResolveAccountType(bool isValuer, bool isSalesAgent, bool isCorporate)
        {
            if (isCorporate)
                return AccountType.Corporate;
            if (isValuer)
                return AccountType.Valuer;
            if (isSalesAgent)
                return AccountType.SalesAgent;
            return AccountType.Standard;
        }

        public void SavePasswordResetLink(Guid userId, string passwordResetLink)
        {
            var user = _userManagerRepository.Get(userId);
            if (user != null)
            {
                user.PasswordResetLink = passwordResetLink;
                user.PasswordResetExpiresOn = DateTimeOffset.UtcNow.AddDays(2);
                Complete();
            }
        }

        public IList<Account> GetUserAccounts()
        {
            return _dbContext.Set<Account>().Where(acc => acc.Active).ToList();
        }

        public bool HasActiveAccount(string userName)
        {
            var user = _userManagerRepository.Find(u => u.Active &&
                string.Equals(u.UserName, userName.Trim()))
                    .Include(u => u.Accounts)
                    .FirstOrDefault();
            if (user == null)
            {
                _logger.LogWarning($"No active user found.");
                return false; 
            }

            if (user.Accounts == null || !user.Accounts.Any())
            {
                _logger.LogWarning($"User has no account record.");
                return false;
            }

            return user.Accounts.Any(ac => ac.Active && ac.ExpiryDate > DateTime.UtcNow);

        }

        public ICollection<Account> GetUserAccounts(Guid userId)
        {
            var user = _userManagerRepository
                .Find(u => u.Id == userId)
                //.AsNoTracking()
                .Include(u => u.Accounts)
                .SingleOrDefault();
            return user?.Accounts;
        }
    }
}