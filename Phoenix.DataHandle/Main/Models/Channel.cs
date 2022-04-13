﻿using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class Channel
    {
        public Channel()
        {
            SchoolLogins = new HashSet<SchoolLogin>();
            UserLogins = new HashSet<UserLogin>();
        }

        public int Id { get; set; }
        public Types.ChannelProvider Provider { get; set; }
        public string ProviderName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<SchoolLogin> SchoolLogins { get; set; }
        public virtual ICollection<UserLogin> UserLogins { get; set; }
    }
}
