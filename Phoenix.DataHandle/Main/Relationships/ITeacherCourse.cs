using Phoenix.DataHandle.Main.Entities;

namespace Phoenix.DataHandle.Main.Relationships
{
    public interface ITeacherCourse
    {
        IAspNetUsers Teacher { get; }
        ICourse Course { get; }
    }
}
