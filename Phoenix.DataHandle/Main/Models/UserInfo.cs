using Phoenix.DataHandle.Identity;
using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class UserInfo
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public bool TermsAccepted { get; set; }
        public bool IsSelfDetermined { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ApplicationUser AppUser { get; set; } = null!;
    }
}
