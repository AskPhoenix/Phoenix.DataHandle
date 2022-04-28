﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Main.Models;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Identity
{
    public class ApplicationContext : IdentityDbContext
        <ApplicationUser,
        ApplicationRole,
        int,
        ApplicationUserClaim,
        ApplicationUserRole,
        ApplicationUserLogin,
        ApplicationRoleClaim,
        ApplicationUserToken>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                // Custom Index for Phone Number
                entity.HasIndex(u => u.PhoneNumber, "PhoneNumberIndex");

                // Custom limit of the Phone Number size
                entity.Property(u => u.PhoneNumber).HasMaxLength(16);

                // Navigation Properties
                entity.HasMany(e => e.Claims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                entity.HasMany(e => e.Logins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                entity.HasMany(e => e.Tokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                entity.HasMany(e => e.Roles)
                    .WithMany(e => e.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserRole",
                        l => l.HasOne<ApplicationRole>().WithMany().HasForeignKey("RoleId").HasConstraintName("FK_AspNetUserRoles_AspNetRoles_RoleId"),
                        r => r.HasOne<ApplicationUser>().WithMany().HasForeignKey("UserId").HasConstraintName("FK_AspNetUserRoles_AspNetUsers_UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });

                entity.HasOne(d => d.User)
                    .WithOne(p => p.AspNetUser)
                    .HasForeignKey<User>(d => d.AspNetUserId)
                    .HasConstraintName("FK_Users_AspNetUsers");
            });

            modelBuilder.Entity<ApplicationRole>(entity =>
            {
                entity.HasMany(e => e.Claims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });
        }
    }
}