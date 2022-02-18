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
        public string? Name { get; set; }
        public string? NormalizedName { get; set; }
        public string? ConcurrencyStamp { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public virtual ICollection<AspNetUser> Users { get; set; }
    }
}
