using System;
using Microsoft.AspNetCore.Identity;
using Phoenix.DataHandle.Main.Entities;

namespace Phoenix.DataHandle.Identity
{
    public sealed class ApplicationUserLogin : IdentityUserLogin<int>, IAspNetUserLogins
    {
        public bool IsActive { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        IAspNetUsers IAspNetUserLogins.User => this.User;
        public ApplicationUser User { get; set; }
    }
}