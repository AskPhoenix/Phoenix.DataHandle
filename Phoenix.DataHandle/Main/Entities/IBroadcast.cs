using Phoenix.DataHandle.Main.Types;
using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IBroadcast
    {
        ISchool School { get; }
        IUserInfo Author { get; }
        string Message { get; }
        DateTime ScheduledFor { get; }
        Daypart Daypart { get; }
        BroadcastAudience Audience { get; }
        BroadcastVisibility Visibility { get; }
        BroadcastStatus Status { get; }
        DateTime? SentAt { get; }

        IEnumerable<ICourse> Courses { get; }
    }
}
