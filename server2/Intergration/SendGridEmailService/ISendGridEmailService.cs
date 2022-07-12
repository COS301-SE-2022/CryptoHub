using Infrastructure.DTO.EmailDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intergration.SendGridEmailService
{
    public interface ISendGridEmailService
    {
        Task SendEmail(EmailDTO emailDTO);
    }
}
