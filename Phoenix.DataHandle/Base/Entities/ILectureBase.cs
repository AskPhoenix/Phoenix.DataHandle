using Phoenix.DataHandle.Main.Types;

namespace Phoenix.DataHandle.Base.Entities
{
    public interface ILectureBase
    {
        DateTimeOffset StartDateTime { get; }
        DateTimeOffset EndDateTime { get; }
        string? OnlineMeetingLink { get; }
        LectureOccasion Occasion { get; }
        bool AttendancesNoted { get; }
        bool IsCancelled { get; }
        string? Comments { get; }
    }
}