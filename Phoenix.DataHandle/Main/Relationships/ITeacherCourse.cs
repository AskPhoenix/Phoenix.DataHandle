using Phoenix.DataHandle.Main.Entities;

namespace Phoenix.DataHandle.Main.Relationships
{
    public interface ITeacherCourse
    {
        IUser Teacher { get; }
        ICourse Course { get; }
    }
}
