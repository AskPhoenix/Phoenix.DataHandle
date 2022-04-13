using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class SchoolLogin
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public int ChannelId { get; set; }
        public string ProviderKey { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime? ActivatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Channel Channel { get; set; } = null!;
        public virtual School School { get; set; } = null!;
    }
}
