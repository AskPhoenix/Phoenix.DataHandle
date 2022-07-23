using Phoenix.DataHandle.Main.Types;

namespace Phoenix.DataHandle.Base
{
    public interface IBroadcastBase
    {
        string Message { get; }
        DateTime ScheduledFor { get; }
        Daypart Daypart { get; }
        BroadcastAudience Audience { get; }
        BroadcastVisibility Visibility { get; }
        BroadcastStatus Status { get; }
        DateTime? SentAt { get; }
    }
}
