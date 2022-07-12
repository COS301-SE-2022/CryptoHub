using Infrastructure.DTO.EmailDTOs;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intergration.SendGridEmailService
{
    public class SendGridEmailService : ISendGridEmailService
    {
        private readonly SendGridClient _sendGridClient;
        private readonly EmailAddress _sourceEmailAddress;

        public SendGridEmailService(IConfiguration configuration)
        {
            var apikey = configuration["SendGrid:Key"];
            _sendGridClient = new SendGridClient(apikey);

            _sourceEmailAddress = new EmailAddress(configuration["SendGrid:Sender"], configuration["SendGrid:Name"]);

        }

        public async Task SendEmail(EmailDTO emailDTO)
        {
            var reciever = new EmailAddress(emailDTO.RecieverEmail, emailDTO.RecieverName);
            var subject = emailDTO.Subject;
            var plaintextContent = emailDTO.plainTextContent;
            var htmlContent = emailDTO.htmlContent;

            var msg = MailHelper
                .CreateSingleEmail
                (_sourceEmailAddress, reciever, 
                subject, plaintextContent, 
                htmlContent);

            await _sendGridClient.SendEmailAsync(msg);
        }
    }
}
