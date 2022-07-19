using Infrastructure.DTO.EmailDTOs;
using Microsoft.Extensions.Configuration;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intergration.SendInBlueEmailService
{
    public class SendInBlueEmailService : ISendInBlueEmailService
    {
        private readonly TransactionalEmailsApi _apiInstance;
        private readonly SendSmtpEmailSender _sender;
        public SendInBlueEmailService(IConfiguration configuration)
        {
            string apiKey = string.Empty;
            var apikey = Configuration.Default.ApiKey.TryGetValue("api-key",out apiKey);

            //if(string.EmptyOr)
            Configuration.Default.ApiKey.Add("api-key", configuration["SendInBlue:Key"]);

            _apiInstance = new TransactionalEmailsApi();
            _sender = new SendSmtpEmailSender(configuration["SendInBlue:Name"],configuration["SendInBlue:Email"]);

        }

        public CreateSmtpEmail Sendemail(EmailDTO email)
        {
            var sendSmtpEmail = new SendSmtpEmail();
            sendSmtpEmail.Sender = _sender;
            sendSmtpEmail.To = new List<SendSmtpEmailTo>
            {
                new SendSmtpEmailTo(email.RecieverEmail,email.RecieverName)
            };
            sendSmtpEmail.Subject = email.Subject;
            sendSmtpEmail.TextContent = email.plainTextContent;

            CreateSmtpEmail result = _apiInstance.SendTransacEmail(sendSmtpEmail);
            return result;
        }
    }
}
