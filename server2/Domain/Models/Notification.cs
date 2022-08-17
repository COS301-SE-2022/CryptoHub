using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public partial class Notification
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int SenderId { get; set; }

        public DateTime LastModified { get; set; }

        public bool IsDeleted { get; set; }

        public virtual User User { get; set; }

        
    }
}
