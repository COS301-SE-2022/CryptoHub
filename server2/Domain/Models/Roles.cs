using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public partial class Roles
    {
        public Roles()
        {
           
        }

        //[Key]
        //[Column("roleid")]
        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;

        //public virtual ICollection<User> Users { get; set; }
    }
}
