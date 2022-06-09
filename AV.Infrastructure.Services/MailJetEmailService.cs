using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using AV.Common.DTOs;
using AV.Common.Interfaces.Services;
using AV.Contracts.Models.Users;

namespace AV.Infrastructure.Services
{
    public class MailJetEmailService : IEmailService
    {
        public async Task SendMail(string toEmailAddress, string subject, string bodyHtml,
            SmtpConfiguration configuration, string ccEmailAddress, EmailTemplate data = null, IEnumerable<EmailAttachment> attachments = null)
        {
            var client = new SmtpClient
            {
                Host = configuration.SmtpMailServer,
                EnableSsl = configuration.UseTLS,
                Credentials = new NetworkCredential(configuration.UserName, configuration.Password),
                Port = configuration.UseTLS ? configuration.Port : configuration.AlternativePort
            };

            var message = new MailMessage(configuration.FromEmailAddress, toEmailAddress);
            if(!string.IsNullOrEmpty(ccEmailAddress))
            {
                message.Bcc.Add(ccEmailAddress);
            }

            if (attachments != null & attachments?.Count() < 6)
            {
                foreach(var attachment in attachments)
                {
                    message.Attachments.Add(ConvertToEmailAttachment(attachment));
                }
            }
            message.IsBodyHtml = true;
            message.Subject = subject;
            if (!string.IsNullOrEmpty(bodyHtml))
            {   
                 message.Body = bodyHtml;
            }
            else
            {
                if (data?.Template == null)
                    throw new InvalidDataException($"Email body not provided or template object has no Template provided.");
                message.Body = SubstituteTemplateValues(data);
            }

            await client.SendMailAsync(message);
        }

        private Attachment ConvertToEmailAttachment(EmailAttachment attachment)
        {
            return new Attachment(attachment.AttachmentDocument, attachment.FileName, attachment.MediaType);
        }

        public static string SubstituteTemplateValues(EmailTemplate data)
        {
            var template = new StringBuilder(data.Template);
            foreach (var dataItem in data.Data)
            {
                template.Replace($"<%#{dataItem.Key}#%>", dataItem.Value);
            }

            return template.ToString();
        }
    }
}
