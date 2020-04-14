using System;
using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Bot.Entities;

namespace Phoenix.DataHandle.Bot
{
    /// <summary>
    /// DbContext for TranscriptEntitys
    /// </summary>
    public class BotTranscriptContext : DbContext
    {
        private string _connectionString;

        /// <summary>
        /// Constructor for TranscriptContext receiving connectionString
        /// </summary>
        /// <param name="connectionString">Connection string to use when configuring the options during <see cref="OnConfiguring"/></param>
        public BotTranscriptContext(string connectionString)
            : base()
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            _connectionString = connectionString;
        }

        /// <summary>
        /// BotTranscript records
        /// </summary>
        public virtual DbSet<BotTranscript> Transcript { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BotTranscript>(entity =>
            {
                entity.ToTable(nameof(BotTranscript));
                entity.HasIndex(e => e.Conversation);
                entity.HasIndex(e => e.Channel);
                entity.HasIndex(e => new { e.Channel, e.Conversation });
                entity.HasKey(e => e.Id);
            });
        }

    }
}
