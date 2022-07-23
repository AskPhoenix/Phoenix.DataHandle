using Phoenix.DataHandle.Base.Entities;

namespace Phoenix.DataHandle.DataEntry.Entities
{
    public interface ICourseAcf : ICourseBase
    {
        IEnumerable<IBookBase> Books { get; }
    }
}
