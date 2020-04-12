using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Phoenix.DataHandle.Models
{
    public partial class PhoenixContext : DbContext
    {
        public PhoenixContext()
        {
        }

        public PhoenixContext(DbContextOptions<PhoenixContext> options)
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
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Phoenix;Persist Security Info=True;User ID=sa;Password=root");
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

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.Property(e => e.created_at).HasColumnType("datetime2(0)");

                entity.Property(e => e.updated_at).HasColumnType("datetime2(0)");
            });

            modelBuilder.Entity<Classroom>(entity =>
            {
                entity.HasIndex(e => new { e.schoolId, e.name })
                    .HasName("UQ__Classroo__A5B5856984D1F366")
                    .IsUnique();

                entity.Property(e => e.created_at).HasColumnType("datetime2(0)");

                entity.Property(e => e.name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.updated_at).HasColumnType("datetime2(0)");

                entity.HasOne(d => d.school)
                    .WithMany(p => p.Classroom)
                    .HasForeignKey(d => d.schoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Classroom__schoo__2F10007B");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasIndex(e => new { e.schoolId, e.level })
                    .HasName("UQ__Course__BE9836D87A0F4518")
                    .IsUnique();

                entity.Property(e => e.created_at).HasColumnType("datetime2(0)");

                entity.Property(e => e.group)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.level)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.updated_at).HasColumnType("datetime2(0)");

                entity.HasOne(d => d.school)
                    .WithMany(p => p.Course)
                    .HasForeignKey(d => d.schoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Course__schoolId__2B3F6F97");
            });

            modelBuilder.Entity<Lecture>(entity =>
            {
                entity.Property(e => e.created_at).HasColumnType("datetime2(0)");

                entity.Property(e => e.endDateTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.startDateTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.updated_at).HasColumnType("datetime2(0)");

                entity.HasOne(d => d.classroom)
                    .WithMany(p => p.Lecture)
                    .HasForeignKey(d => d.classroomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Lecture__classro__398D8EEE");

                entity.HasOne(d => d.course)
                    .WithMany(p => p.Lecture)
                    .HasForeignKey(d => d.courseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Lecture__courseI__38996AB5");
            });

            modelBuilder.Entity<School>(entity =>
            {
                entity.HasIndex(e => e.slug)
                    .HasName("SchoolSlugIndex")
                    .IsUnique();

                entity.Property(e => e.addressLine)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.city)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.created_at).HasColumnType("datetime2(0)");

                entity.Property(e => e.name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.slug)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.updated_at).HasColumnType("datetime2(0)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.aspNetUserId);

                entity.Property(e => e.aspNetUserId).ValueGeneratedNever();

                entity.Property(e => e.forename).HasMaxLength(255);

                entity.Property(e => e.surname).HasMaxLength(255);

                entity.HasOne(d => d.aspNetUser)
                    .WithOne(p => p.User)
                    .HasForeignKey<User>(d => d.aspNetUserId)
                    .HasConstraintName("FK__User__aspNetUser__276EDEB3");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
