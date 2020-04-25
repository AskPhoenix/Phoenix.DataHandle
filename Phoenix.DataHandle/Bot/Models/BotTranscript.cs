using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoenix.DataHandle.Bot.Models
{
    /// <summary>
    /// BotTranscript representing one Bot Activity record.
    /// </summary>
    [Table("BotTranscript")]
    public class BotTranscript
    {
        /// <summary>
        /// Constructor for BotTranscript
        /// </summary>
        /// <remarks>
        /// Sets Timestamp to DateTimeOffset.UtfcNow
        /// </remarks>
        public BotTranscript()
        {
            Timestamp = DateTimeOffset.UtcNow;
        }

        /// <summary>
        /// Gets or sets the auto-generated Id/Key.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Channel this activity occurred on.
        /// </summary>
        [MaxLength(256)]
        public string Channel { get; set; }

        /// <summary>
        /// Gets or sets the Conversation id this activity occurred on.
        /// </summary>
        [MaxLength(1024)]
        public string Conversation { get; set; }

        /// <summary>
        /// Gets or sets the persisted Activity as a string.
        /// </summary>
        [Column(TypeName = "nvarchar(MAX)")]
        public string Activity { get; set; }

        /// <summary>
        /// Gets or sets the current timestamp.
        /// </summary>
        [Required]
        [Timestamp]
        public DateTimeOffset Timestamp { get; set; }
    }
}
