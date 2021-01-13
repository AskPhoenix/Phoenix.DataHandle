using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Main.Models;
using System;

namespace Phoenix.DataHandle.Identity
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
                throw new ArgumentNullException(nameof(modelBuilder));

            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.HasOne(a => a.User)
                .WithOne()
                .HasForeignKey<User>(b => b.AspNetUserId);
            });

            modelBuilder.Entity<ApplicationUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.CreatedAt).HasColumnType("datetimeoffset(0)");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetimeoffset(0)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ApplicationUserLogin)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_AspNetUserLogins_AspNetUsers");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.AspNetUserId);
                entity.Property(e => e.AspNetUserId).ValueGeneratedNever();
                entity.Property(e => e.LastName).HasMaxLength(255);
                entity.Property(e => e.FirstName).HasMaxLength(255);
            });

            modelBuilder.Ignore<AspNetUsers>();
            modelBuilder.Ignore<Attendance>();
            modelBuilder.Ignore<BotFeedback>();
            modelBuilder.Ignore<StudentCourse>();
            modelBuilder.Ignore<StudentExam>();
            modelBuilder.Ignore<StudentExercise>();
            modelBuilder.Ignore<TeacherCourse>();
        }
    }
}