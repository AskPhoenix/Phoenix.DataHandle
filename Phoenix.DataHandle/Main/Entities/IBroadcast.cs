using Phoenix.DataHandle.Base;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IBroadcast : IBroadcastBase
    {
        ISchool School { get; }
        IUser Author { get; }

        IEnumerable<ICourse> Courses { get; }
    }
}
