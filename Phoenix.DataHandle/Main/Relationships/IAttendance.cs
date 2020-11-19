using Phoenix.DataHandle.Main.Entities;

namespace Phoenix.DataHandle.Main.Relationships
{
    public interface IAttendance
    {
        IAspNetUsers Student { get; }
        ILecture Lecture { get; }
    }
}