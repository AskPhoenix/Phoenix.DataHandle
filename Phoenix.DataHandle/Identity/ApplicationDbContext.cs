using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Entities;
using Phoenix.DataHandle.Models;

namespace Phoenix.DataHandle.Identity
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(a => a.User)
                .WithOne()
                .HasForeignKey<User>(b => b.AspNetUserId);

            modelBuilder.Ignore<AspNetUsers>();
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.AspNetUserId);
                entity.Property(e => e.AspNetUserId).ValueGeneratedNever();
                entity.Property(e => e.LastName).HasMaxLength(255);
                entity.Property(e => e.FirstName).HasMaxLength(255);
            });

        }

    }

    public sealed class ApplicationUser : IdentityUser<int>, IAspNetUser
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        IUser IAspNetUser.User => this.User;
        public User User { get; set; }
        public string FacebookId { get; set; }

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

    public class ApplicationRole : IdentityRole<int>
    {
    }

}