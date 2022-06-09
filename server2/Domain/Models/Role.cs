using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public partial class Role
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public int RoleId { get; set; }
        public string Role1 { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<UserRole>? UserRoles { get; set; }
    }
}
