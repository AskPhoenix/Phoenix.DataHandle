using Phoenix.DataHandle.Base.Entities;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface ISchedule : IScheduleBase
    {
        ICourse Course { get; }
        IClassroom? Classroom { get; }

        IEnumerable<ILecture> Lectures { get; }
    }
}