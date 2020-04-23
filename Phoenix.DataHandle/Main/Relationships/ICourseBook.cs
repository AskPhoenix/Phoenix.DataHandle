using Phoenix.DataHandle.Main.Entities;

namespace Phoenix.DataHandle.Main.Relationships
{
    public interface ICourseBook
    {
        ICourse Course { get; }
        IBook Book { get; }
    }
}
