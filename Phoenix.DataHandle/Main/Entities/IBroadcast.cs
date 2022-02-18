using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IBroadcast
    {
        ISchool School { get; }
        IAspNetUser Author { get; }
        string Message { get; set; }
        DateTimeOffset ScheduledDate { get; set; }
        Daypart Daypart { get; set; }
        BroadcastAudience Audience { get; set; }
        BroadcastVisibility Visibility { get; set; }
        BroadcastStatus Status { get; set; }
        DateTimeOffset? SentAt { get; set; }

        IEnumerable<ICourse> Courses { get; }
    }
}
