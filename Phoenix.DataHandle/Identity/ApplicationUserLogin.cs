using System;
using Microsoft.AspNetCore.Identity;
using Phoenix.DataHandle.Main.Entities;

namespace Phoenix.DataHandle.Identity
{
    public sealed class ApplicationUserLogin : IdentityUserLogin<int>, IAspNetUserLogins
    {
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public string OneTimeCode { get; set; }
        public bool OTCUsed { get; set; }
        public DateTimeOffset? OTCCreatedAt { get; set; }

        IAspNetUsers IAspNetUserLogins.User => this.User;
        public ApplicationUser User { get; set; }
    }
}