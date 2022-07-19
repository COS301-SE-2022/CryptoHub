using Infrastructure.DTO.EmailDTOs;
using sib_api_v3_sdk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intergration.SendInBlueEmailService
{
    public interface ISendInBlueEmailService
    {
        CreateSmtpEmail Sendemail(EmailDTO email);
    }
}
