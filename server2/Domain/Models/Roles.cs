using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Roles
    {
        public Roles()
        {
           
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;

        //public virtual ICollection<User> Users { get; set; }
    }
}
