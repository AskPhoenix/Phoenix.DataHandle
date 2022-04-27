using Microsoft.AspNetCore.Identity;
using Phoenix.DataHandle.Main.Models;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public ApplicationUser()
            : base()
        {
            Claims = new HashSet<ApplicationUserClaim>();
            Logins = new HashSet<ApplicationUserLogin>();
            Tokens = new HashSet<ApplicationUserToken>();
            Roles = new HashSet<ApplicationRole>();
        }

        public virtual UserInfo UserInfo { get; set; } = null!;

        public virtual ICollection<ApplicationUserClaim> Claims { get; set; }
        public virtual ICollection<ApplicationUserLogin> Logins { get; set; }
        public virtual ICollection<ApplicationUserToken> Tokens { get; set; }
        public virtual ICollection<ApplicationRole> Roles { get; set; }

        public void Normalize()
        {
            this.NormalizedEmail = this.Email?.ToUpperInvariant();
            this.NormalizedUserName = this.UserName?.ToUpperInvariant();
        }
    }
}