using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public partial class Message
    {
        public int Id { get; set; }

        public int SenderId { get; set; }
        public int RecieverId {get; set; }

        public string? Content  { get; set; } = string.Empty;


        public virtual User Sender { get; set; }

        public virtual User Reciever { get; set; }
    }
}
