﻿using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class AspNetUserLogin
    {
        public int UserId { get; set; }
        public int ChannelId { get; set; }
        public string ProviderKey { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public virtual Channel Channel { get; set; } = null!;
        public virtual AspNetUser User { get; set; } = null!;
    }
}