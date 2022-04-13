using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public Types.RoleRank Rank { get; set; }
        public string Name { get; set; } = null!;
        public string? ConcurrencyStamp { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
