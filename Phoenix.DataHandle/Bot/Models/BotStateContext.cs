using System;
using Microsoft.EntityFrameworkCore;

namespace Phoenix.DataHandle.Bot.Models
{
    /// <summary>
    /// DbContext for TranscriptEntitys
    /// </summary>
    public class BotStateContext : DbContext
    {
        private string _connectionString;

        /// <summary>
        /// Constructor for PhoenixBotContext receiving connectionString
        /// </summary>
        /// <param name="connectionString">Connection string to use when configuring the options during <see cref="OnConfiguring"/></param>
        public BotStateContext(string connectionString)
            : base()
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            this._connectionString = connectionString;
        }

        /// <summary>
        /// Constructor for PhoenixBotContext receiving DBContextOptions
        /// </summary>
        /// <param name="options">Options to use for configuration.</param>
        public BotStateContext(DbContextOptions<BotStateContext> options)
            : base(options)
        { }

        /// <summary>
        /// BotTranscript records
        /// </summary>
        public virtual DbSet<BotTranscript> Transcript { get; set; }

        /// <summary>
        /// BotData records
        /// </summary>
        public virtual DbSet<BotData> BotData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(this._connectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BotData>(entity =>
            {
                entity.ToTable(nameof(this.BotData));
                entity.HasIndex(e => e.RealId);
                entity.HasKey(e => e.Id);
            });

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
