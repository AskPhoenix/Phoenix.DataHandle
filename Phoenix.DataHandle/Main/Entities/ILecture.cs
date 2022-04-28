using Phoenix.DataHandle.Main.Types;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface ILecture
    {
        ICourse Course { get; }
        IClassroom? Classroom { get; }
        ISchedule? Schedule { get; }
        DateTimeOffset StartDateTime { get; }
        DateTimeOffset EndDateTime { get; }
        string? OnlineMeetingLink { get; }
        LectureOccasion Occasion { get; }
        bool AttendancesNoted { get; }
        bool IsCancelled { get; }
        ILecture? ReplacementLecture { get; }
        string? Comments { get; }

        IEnumerable<IExam> Exams { get; }
        IEnumerable<IExercise> Exercises { get; }
        IEnumerable<ILecture> InverseReplacementLecture { get; }

        IEnumerable<IUser> Attendees { get; }
    }
}