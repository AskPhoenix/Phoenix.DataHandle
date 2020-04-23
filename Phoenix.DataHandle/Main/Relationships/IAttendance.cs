using Phoenix.DataHandle.Main.Entities;

namespace Phoenix.DataHandle.Main.Relationships
{
    public interface IAttendance
    {
        IUser Student { get; }
        ILecture Lecture { get; }
    }
}