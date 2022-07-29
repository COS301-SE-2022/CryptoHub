using Infrastructure.DTO.EmailDTOs;
using Infrastructure.Setting;
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
            var apikey = configuration["SendInBlue:Key"];
            _sendGridClient = new SendGridClient(apikey);

            
            _sourceEmailAddress = new EmailAddress(FireStoreSettings.Email, configuration["SendInBlue:Name"]);

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
