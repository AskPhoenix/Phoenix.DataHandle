using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class UserLogin
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ChannelId { get; set; }
        public string ProviderKey { get; set; } = null!;
        public int VerificationOneTimeCodeId { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ActivatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Channel Channel { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual OneTimeCode VerificationOneTimeCode { get; set; } = null!;
    }
}
