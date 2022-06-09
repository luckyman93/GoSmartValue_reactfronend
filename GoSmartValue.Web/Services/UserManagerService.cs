using AutoMapper;
using AV.Common;
using AV.Common.DTOs;
using AV.Common.Entities;
using AV.Common.Interfaces;
using AV.Common.Interfaces.Repositories;
using AV.Common.Interfaces.Services;
using AV.Common.Interfaces.UnitOfWorks;
using AV.Contracts.Enums;
using AV.Contracts.Models;
using AV.Contracts.Models.Accounts;
using AV.Contracts.Models.Payment.Requests;
using AV.Contracts.Models.Product;
using AV.Contracts.Models.Product.Requests;
using GoSmartValue.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Account = AV.Common.Entities.Account;
using User = AV.Common.Entities.User;
using UserModel = AV.Contracts.Models.Users.UserModel;

namespace GoSmartValue.Web.Services
{
    public class UserManagerService : IUserManagerService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserManagerUnitOfWork _userManagerUnitOfWork;
        private readonly IOrganisationUnitOfWork _organisationUnitOfWork;
        private readonly IUserManagerRepository _userManagerRepository;
        private readonly IAccountsRepository _accountsRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<UserManagerService> _logger;
        private readonly IEmailService _emailService;
        private readonly IOptions<SmtpConfiguration> _smtpOptions;

        public UserManagerService(
            IHttpContextAccessor httpContextAccessor,
            IUserManagerUnitOfWork userManagerUnitOfWork,
            IOrganisationUnitOfWork organisationUnitOfWork,
            IUserManagerRepository userManagerRepository,
            IAccountsRepository accountsRepository,
            UserManager<User> userManager,
            IMediator mediator,
            IMapper mapper,
            ILogger<UserManagerService> logger,
            IEmailService emailService,
            IOptions<SmtpConfiguration> smtpOptions)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManagerUnitOfWork = userManagerUnitOfWork;
            _organisationUnitOfWork = organisationUnitOfWork;
            _userManagerRepository = userManagerRepository;
            _accountsRepository = accountsRepository;
            _userManager = userManager;
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
            _emailService = emailService;
            _smtpOptions = smtpOptions;
        }

        public Guid GetLoggedInUserIdAsync()
        {
            var userId = _httpContextAccessor.HttpContext
                .User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;
            return Guid.Parse(userId);
        }

        public IList<UserModel> GetAllUsers(bool activeOnly = true)
        {
            var users = _userManagerUnitOfWork.GetAllUsers(activeOnly);

            return _mapper.Map<List<UserModel>>(users);
        }

        public Task<UserModel> GetUserDetails(Guid userId)
        {
            var user = _userManagerUnitOfWork.GetUser(userId);
            user.Accounts = _userManagerUnitOfWork.GetUserAccounts(userId);

            return Task.FromResult(_mapper.Map<UserModel>(user));
        }

        public User GetUserDetails(string userName)
        {
            var task = Task.Run(async () => await _userManagerUnitOfWork.GetUserByUserName(userName));
            return task.Result;
        }

        public void CreateUserAccountRecord(User user, SubscriptionType subscriptionType, bool isValuer = false, bool isSalesAgent = false,
            bool isCorporate = false, bool isBanker = false, bool isInsurance = false, bool isGovernmentAgency = false,
            bool isDeveloper = false, string companyName = null)
        {
            _userManagerUnitOfWork.CreateAccountRecord(user.Id, subscriptionType,
                isValuer,
                isSalesAgent,
                isCorporate,
                isBanker,
                isInsurance,
                isGovernmentAgency,
                isDeveloper,
                companyName);
        }

        public IList<OrganisationViewModel> GetOrganistions(Guid userId, bool isAdmin = false)
        {
            var organisations = _organisationUnitOfWork.GetAll()
                .Where(o => o.PrimaryUserId == userId || isAdmin).ToList();
            return _mapper.Map<List<OrganisationViewModel>>(organisations);
        }

        public OrganisationViewModel GetOrganisation(Guid userId)
        {
            var organisation = _organisationUnitOfWork.GetByUserId(userId);
            return _mapper.Map<OrganisationViewModel>(organisation);
        }

        public void EditOrganisation(OrganisationViewModel organisation)
        {
            var company = _mapper.Map<Organisation>(organisation);
            _organisationUnitOfWork.SaveOrganisation(company);
        }

        public void CreateOrganisation(OrganisationViewModel organisation)
        {
            var company = _mapper.Map<Organisation>(organisation);
            _organisationUnitOfWork.SaveOrganisation(company);
        }

        public void SavePasswordResetLink(Guid userId, string passwordResetLink)
        {
            _userManagerUnitOfWork.SavePasswordResetLink(userId, passwordResetLink);

        }

        public IEnumerable<AccountViewModel> GetAllAccounts()
        {
            var userAccounts = _userManagerUnitOfWork.GetUserAccounts();
            return _mapper.Map<List<AccountViewModel>>(userAccounts);
        }

        public bool HasActiveAccount(string userName)
        {
            return _userManagerUnitOfWork.HasActiveAccount(userName);
        }

        public bool UserOnAccount(User currentUser, Guid ownerId)
        {
            var account = _userManagerUnitOfWork.GetUserAccounts(ownerId)
                .FirstOrDefault(ac => ac.Active && ac.ExpiryDate > DateTime.Now);
            if (account == default)
            {
                return false;
            }
            var currentUserAccount =
                _userManagerUnitOfWork.GetUserAccounts(currentUser.Id)
                .FirstOrDefault(ac => ac.Active && ac.ExpiryDate > DateTime.Now);
            if (currentUserAccount == default)
            {
                return false;
            }
            return account.Id == currentUserAccount.Id;
        }

        public void ResetReturnUrl(User user)
        {
            _userManagerUnitOfWork.ResetReturnUrl(user);
        }

        public async Task<bool> CanTransactAsync(Guid id, SubscriptionType subType)
        {
            var currentUser = await _userManagerRepository.GetAsync(id);

            var account = currentUser.Accounts
                .FirstOrDefault(ac => ac.SubscriptionType == subType && ac.Active && ac.ExpiryDate > DateTimeOffset.Now);
            //user.Accounts.Add(account);
            //var account = user.Accounts.FirstOrDefault();
            return CanTransact(account);
        }
        private bool CanTransact(Account account)
        {
            if (account == null)
            {
                return false;
            }
            if (!account.Active)
            {
                return false;
            }

            return HasCredit(account);
        }
        private bool HasCredit(Account account)
        {
            //Use the account type to determine subscription 
            //Only valid for transactional and if account has expired subscription type
            if (account.SubscriptionType != SubscriptionType.SingleUse && account.ExpiryDate > DateTime.UtcNow)
            {
                return true;
            }
            if (account.SubscriptionType == SubscriptionType.SingleUse && account.Balance > 500)
            {
                return true;
            }
            return false;
        }

        public async Task<MakePaymentRequest> CreateMakePaymentRequest(User user, SubscriptionType subscriptionType)
        {
            var product = await GetProductPrice(subscriptionType.ToString());
            return new MakePaymentRequest
            {
                InitiatedByUserId = user.Id,
                AccountId = user.Accounts.FirstOrDefault(a => a.Active).Id,
                Reference = user.Id.ToString(),
                Currency = Constants.Currency.Botswana.Code,
                Type = PaymentType.Subscription,
                Amount = (int)product.Price,
                ServiceName = product.Description,
                ServiceType = product.ServiceType
            };
        }
        private async Task<ProductModel> GetProductPrice(string productName)
        {
            return await _mediator.Send(new GetProductDetailsRequest
            {
                Name = productName,
            });
        }

        public async Task<Guid> GetActiveAccountId(Guid id)
        {
            var account = await _accountsRepository.GetUserAccount(id);
            return account.Id;
        }

        public async Task SendAccountConfirmationMail(UserModel user)
        {
            try
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(_mapper.Map<User>(user));

                var confirmationEmailUrl = $"{Startup.EnvironmentVariables["Hostname"]}/Account/EmailConfirmation?email={user.Email}";
                _logger.LogInformation($"ConfirmationEmailLink: {confirmationEmailUrl}");

                await _emailService.SendMail(user.Email, "Email Confirmation - goSmartValue.com", null,
                    _smtpOptions.Value,
                    null,
                    new EmailTemplate
                    {
                        Data = new Dictionary<string, string>()
                        {
                            {"activationLink", confirmationEmailUrl},
                            {"logoImageUr",$"{Startup.EnvironmentVariables["Hostname"]}/gosmartvalue.png"},
                            {"firstName", user.FirstName},
                            {"fastName", user.LastName}
                        },
                        Template = TemplateConstants.TemplateAccountActivation
                    });
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error,
                    $"Unable send email for account activation for User:{user.Id}: Error: {exception.Message}");
            }
        }

        public async Task SendForgotPasswordEmailAsync(UserModel user, string callbackUrl)
        {

            _logger.LogInformation($"ForgotPasswordLink: {callbackUrl}");

            await _emailService.SendMail(user.Email, "Password Reset - goSmartValue.com", null,
                _smtpOptions.Value,
                null,
                new EmailTemplate
                {
                    Data = new Dictionary<string, string>()
                    {
                        {"resetLink", callbackUrl},
                        {"logoImageUr","www.gosmartvalue.com/gosmartvalue.png"},
                        {"firstName", user.FirstName},

                        {"fastName", user.LastName}
                    },
                    Template = TemplateConstants.TemplateAccountPasswordReset
                });
        }
    }
}