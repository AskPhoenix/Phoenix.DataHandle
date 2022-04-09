using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class Channel
    {
        public Channel()
        {
            AspNetUserLogins = new HashSet<AspNetUserLogin>();
            SchoolLogins = new HashSet<SchoolLogin>();
        }

        public int Id { get; set; }
        public int Code { get; set; }
        public string Provider { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual ICollection<SchoolLogin> SchoolLogins { get; set; }
    }
}
