using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IBroadcast
    {
        ISchool School { get; }
        IAspNetUser Author { get; }
        string Message { get; }
        DateTimeOffset ScheduledDate { get; }
        Daypart Daypart { get; }
        BroadcastAudience Audience { get; }
        BroadcastVisibility Visibility { get; }
        BroadcastStatus Status { get; }
        DateTimeOffset? SentAt { get; }

        IEnumerable<ICourse> Courses { get; }
    }
}
