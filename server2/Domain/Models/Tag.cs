using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Tag
    {

        public Tag()
        {
            PostTags = new HashSet<PostTag>();
        }

        public int TagId { get; set; }
        public string? Content { get; set; }

        public virtual ICollection<PostTag> PostTags { get; set; }
    }
}
