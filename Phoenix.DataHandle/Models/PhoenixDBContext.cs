using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Phoenix.DataHandle.Models
{
    public partial class PhoenixDBContext : DbContext
    {
        public PhoenixDBContext()
        {
        }

        public PhoenixDBContext(DbContextOptions<PhoenixDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Classroom> Classroom { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Lecture> Lecture { get; set; }
        public virtual DbSet<School> School { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:askphoenix.database.windows.net,1433;Initial Catalog=PhoenixDB;Persist Security Info=False;User ID=phoenix;Password=20Ph0eniX20!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime2(0)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime2(0)");

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Classroom>(entity =>
            {
                entity.HasIndex(e => new { e.SchoolId, e.Name })
                    .HasName("UQ__Classroo__A5B5856930D0EBE6")
                    .IsUnique();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime2(0)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime2(0)");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Classroom)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Classroom__schoo__66603565");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasIndex(e => new { e.SchoolId, e.Level })
                    .HasName("UQ__Course__BE9836D8D4139A41")
                    .IsUnique();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime2(0)");

                entity.Property(e => e.Group)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Level)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime2(0)");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Course)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Course__schoolId__6754599E");
            });

            modelBuilder.Entity<Lecture>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime2(0)");

                entity.Property(e => e.EndDateTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.StartDateTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime2(0)");

                entity.HasOne(d => d.Classroom)
                    .WithMany(p => p.Lecture)
                    .HasForeignKey(d => d.ClassroomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Lecture__classro__693CA210");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Lecture)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Lecture__courseI__68487DD7");
            });

            modelBuilder.Entity<School>(entity =>
            {
                entity.HasIndex(e => e.Slug)
                    .HasName("SchoolSlugIndex")
                    .IsUnique();

                entity.Property(e => e.AddressLine)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime2(0)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Slug)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime2(0)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.AspNetUserId);

                entity.Property(e => e.AspNetUserId).ValueGeneratedNever();

                entity.Property(e => e.Surname).HasMaxLength(255);

                entity.Property(e => e.Forename).HasMaxLength(255);

                entity.HasOne(d => d.AspNetUser)
                    .WithOne(p => p.User)
                    .HasForeignKey<User>(d => d.AspNetUserId)
                    .HasConstraintName("FK__User__aspNetUser__6A30C649");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
