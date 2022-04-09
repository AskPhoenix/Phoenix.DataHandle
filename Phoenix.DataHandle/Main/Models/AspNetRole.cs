using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class AspNetRole
    {
        public AspNetRole()
        {
            Users = new HashSet<AspNetUser>();
        }

        public int Id { get; set; }
        public Role Type { get; set; }
        public string Name { get; set; } = null!;
        public string? ConcurrencyStamp { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<AspNetUser> Users { get; set; }
    }
}
