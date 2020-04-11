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

        public virtual DbSet<School> School { get; set; }

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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
