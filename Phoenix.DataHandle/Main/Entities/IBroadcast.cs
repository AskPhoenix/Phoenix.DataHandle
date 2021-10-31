using System;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IBroadcast
    {
        ISchool School { get; }
        string Message { get; set; }
        DateTimeOffset ScheduledDate { get; set; }
        Daypart Daypart { get; set; }
        BroadcastAudience Audience { get; set; }
        BroadcastVisibility Visibility { get; set; }
        ICourse Course { get; }
        BroadcastStatus Status { get; set; }
        IAspNetUsers CreatedByUser { get; }
        DateTimeOffset? SentAt { get; set; }
    }
}
