using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Tag
    {
        public int TagId { get; set; }
        public int PostId { get; set; }
        public string? Content { get; set; }

        public virtual Post Post { get; set; } = null!;
    }
}
