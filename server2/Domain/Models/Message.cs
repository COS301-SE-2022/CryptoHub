﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public partial class Message
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int RecieverId {get; set; }

        public string? Content  { get; set; } = string.Empty;

        public DateTime TimeDelivered { get; set; }

        public bool Read { get; set; } = false;

        public virtual User User { get; set; }
    }
}
