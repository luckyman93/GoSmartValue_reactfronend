using AV.Common.Entities;
using AV.Contracts.Enums;
using AV.Contracts.Models.Accounts;
using AV.Contracts.Models.Payment.Requests;
using AV.Contracts.Models.Users;
using GoSmartValue.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoSmartValue.Web.Services
{
    public interface IUserManagerService
    {
        Guid GetLoggedInUserIdAsync();
        IList<UserModel> GetAllUsers(bool activeOnly = true);
        Task<UserModel> GetUserDetails(Guid userId);
        User GetUserDetails(string userName);
        void CreateUserAccountRecord(User user,
            SubscriptionType subscriptionType,
            bool isValuer = false,
            bool isSalesAgent = false,
            bool isCorporate = false,
            bool isBanker = false,
            bool isInsurance = false,
            bool isGovernmentAgency = false,
            bool isDeveloper = false,
            string companyName = null);
        IList<OrganisationViewModel> GetOrganistions(Guid userId, bool isAdmin = false);
        OrganisationViewModel GetOrganisation(Guid userId);
        void EditOrganisation(OrganisationViewModel organisation);
        void CreateOrganisation(OrganisationViewModel organisation);
        void SavePasswordResetLink(Guid userId, string passwordResetLink);
        IEnumerable<AccountViewModel> GetAllAccounts();
        bool HasActiveAccount(string userName);
        bool UserOnAccount(User currentUser, Guid ownerId);
        void ResetReturnUrl(User currentUser);
        Task<bool> CanTransactAsync(Guid id, SubscriptionType subType);
        Task<MakePaymentRequest> CreateMakePaymentRequest(User user, SubscriptionType subscriptionType);
        Task<Guid> GetActiveAccountId(Guid id);
        Task SendAccountConfirmationMail(UserModel user);
        Task SendForgotPasswordEmailAsync(UserModel userModel, string callbackUrl);
    }
}
