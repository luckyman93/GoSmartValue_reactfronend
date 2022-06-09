using System.Threading.Tasks;
using AV.Common.Interfaces.Services;
using AV.Contracts.Interface;
using AV.Contracts.Models.Users;
using Microsoft.Extensions.Logging;

namespace GoSmartValue.Web.Services
{
    public class UserNotificationService: IUserNotificationService
    {
        private readonly IEmailService _emailService;
        private readonly IUserManagerService _userManagerService;
        private readonly ILogger<UserNotificationService> _logger;

        public UserNotificationService(
            IEmailService emailService,
            IUserManagerService userManagerService,
            ILogger<UserNotificationService> logger)
        {
            _emailService = emailService;
            _userManagerService = userManagerService;
            _logger = logger;
        }

        public async Task SendConfirmationEmail(UserModel user)
        {
            await _userManagerService.SendAccountConfirmationMail(user);
        }
    }
}
