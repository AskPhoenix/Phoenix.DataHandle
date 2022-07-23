using Phoenix.DataHandle.Base;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface ILecture : ILectureBase
    {
        ICourse Course { get; }
        IClassroom? Classroom { get; }
        ISchedule? Schedule { get; }
        ILecture? ReplacementLecture { get; }

        IEnumerable<IExam> Exams { get; }
        IEnumerable<IExercise> Exercises { get; }
        IEnumerable<ILecture> InverseReplacementLecture { get; }

        IEnumerable<IUser> Attendees { get; }
    }
}