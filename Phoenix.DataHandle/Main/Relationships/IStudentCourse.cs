using Phoenix.DataHandle.Main.Entities;

namespace Phoenix.DataHandle.Main.Relationships
{
    public interface IStudentCourse
    {
        IUser Student { get; }
        ICourse Course { get; }
        decimal? Grade { get; set; }
    }
}