using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class Broadcast
    {
        public Broadcast()
        {
            Courses = new HashSet<Course>();
        }

        public int Id { get; set; }
        public int SchoolId { get; set; }
        public int AuthorId { get; set; }
        public string Message { get; set; } = null!;
        public DateTimeOffset ScheduledFor { get; set; }
        public Daypart Daypart { get; set; }
        public BroadcastAudience Audience { get; set; }
        public BroadcastVisibility Visibility { get; set; }
        public BroadcastStatus Status { get; set; }
        public DateTime? SentAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual AspNetUser Author { get; set; } = null!;
        public virtual School School { get; set; } = null!;

        public virtual ICollection<Course> Courses { get; set; }
    }
}
