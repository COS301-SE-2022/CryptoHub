using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO.EmailDTOs
{
    public class EmailDTO
    {
        public string? Subject { get; set; }

        public string RecieverEmail { get; set; }

        public string? RecieverName { get; set; }

        public string? plainTextContent { get; set; }

        public string? htmlContent { get; set; }

    }
}
