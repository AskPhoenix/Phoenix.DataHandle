using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Phoenix.DataHandle.Main.Models
{
    public partial class Broadcast
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public string Message { get; set; }
        public DateTimeOffset ScheduledDate { get; set; }
        public Daypart Daypart { get; set; }
        public BroadcastAudience Audience { get; set; }
        public BroadcastVisibility Visibility { get; set; }
        public int? CourseId { get; set; }
        public BroadcastStatus Status { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTimeOffset? SentAt { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public virtual Course Course { get; set; }
        public virtual AspNetUsers CreatedByUser { get; set; }
        public virtual School School { get; set; }
    }
}
