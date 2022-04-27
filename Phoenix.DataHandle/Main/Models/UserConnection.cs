using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class UserConnection
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Channel { get; set; } = null!;
        public string ChannelKey { get; set; } = null!;
        public string ChannelDisplayName { get; set; } = null!;
        public DateTime? ActivatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual UserInfo Tenant { get; set; } = null!;
    }
}
