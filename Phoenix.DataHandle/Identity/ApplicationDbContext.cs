using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Identity
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

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
            modelBuilder.Ignore<Attendance>();
            modelBuilder.Ignore<BotFeedback>();
            modelBuilder.Ignore<StudentCourse>();
            modelBuilder.Ignore<StudentExam>();
            modelBuilder.Ignore<StudentExercise>();
            modelBuilder.Ignore<TeacherCourse>();

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.AspNetUserId);
                entity.Property(e => e.AspNetUserId).ValueGeneratedNever();
                entity.Property(e => e.LastName).HasMaxLength(255);
                entity.Property(e => e.FirstName).HasMaxLength(255);
            });
        }
    }
}