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
        public int? AuthorId { get; set; }
        public string Message { get; set; } = null!;
        public DateTime ScheduledFor { get; set; }
        public Types.Daypart Daypart { get; set; }
        public Types.BroadcastAudience Audience { get; set; }
        public Types.BroadcastVisibility Visibility { get; set; }
        public Types.BroadcastStatus Status { get; set; }
        public DateTime? SentAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual User? Author { get; set; }
        public virtual School School { get; set; } = null!;

        public virtual ICollection<Course> Courses { get; set; }
    }
}
