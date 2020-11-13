using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Phoenix.DataHandle.Main;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Main.Relationships;

namespace Phoenix.DataHandle.Identity
{
    public sealed class ApplicationUser : IdentityUser<int>, IAspNetUsers
    {
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset RegisteredAt => this.CreatedAt;
        public ApplicationType CreatedApplicationType { get; set; }

        IUser IAspNetUsers.User => this.User;
        public User User { get; set; }
        public IEnumerable<IAspNetUserRoles> Roles { get; }
        public IEnumerable<IUserSchool> UserSchool { get; }

        public IEnumerable<IAspNetUserLogins> AspNetUserLogins { get; }

        public ApplicationUser() { }

        public ApplicationUser(IdentityUser<int> identity) : this()
        {
            this.Id = identity.Id;
            this.AccessFailedCount = identity.AccessFailedCount;
            this.ConcurrencyStamp = identity.ConcurrencyStamp;
            this.Email = identity.Email;
            this.EmailConfirmed = identity.EmailConfirmed;
            this.LockoutEnabled = identity.LockoutEnabled;
            this.LockoutEnd = identity.LockoutEnd;
            this.NormalizedEmail = identity.NormalizedEmail;
            this.NormalizedUserName = identity.NormalizedUserName;
            this.PasswordHash = identity.PasswordHash;
            this.SecurityStamp = identity.SecurityStamp;
            this.PhoneNumber = identity.PhoneNumber;
            this.PhoneNumberConfirmed = identity.PhoneNumberConfirmed;
            this.UserName = identity.UserName;
            this.TwoFactorEnabled = identity.TwoFactorEnabled;
        }
    }
}