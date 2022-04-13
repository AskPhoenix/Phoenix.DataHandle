using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class OneTimeCode
    {
        public OneTimeCode()
        {
            UserInfos = new HashSet<UserInfo>();
            UserLogins = new HashSet<UserLogin>();
        }

        public int Id { get; set; }
        public string Token { get; set; } = null!;
        public Types.OneTimeCodePurpose Purpose { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<UserInfo> UserInfos { get; set; }
        public virtual ICollection<UserLogin> UserLogins { get; set; }
    }
}
