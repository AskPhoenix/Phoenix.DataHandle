using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Phoenix.DataHandle.Bot.Models
{
    public partial class PhoenixBotContext : DbContext
    {
        public PhoenixBotContext()
        {
        }

        public PhoenixBotContext(DbContextOptions<PhoenixBotContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BotFeedback> BotFeedback { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                throw new Exception("Connection string not specified for PhoenixBotContext.");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BotFeedback>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Topic)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
