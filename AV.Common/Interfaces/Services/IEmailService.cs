using AV.Common.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AV.Common.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendMail(
            string toEmailAddress,
            string subject,
            string bodyHtml,
            SmtpConfiguration configuration,
            string ccEmailAddress = null,
            EmailTemplate data = null,
            IEnumerable<EmailAttachment> attachments = null);
    }
}
