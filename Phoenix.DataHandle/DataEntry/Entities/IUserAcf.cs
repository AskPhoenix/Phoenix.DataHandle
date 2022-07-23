using Phoenix.DataHandle.Base.Entities;

namespace Phoenix.DataHandle.DataEntry.Entities
{
    public interface IUserAcf : IUserBase
    {
        string PhoneString { get; }

        IEnumerable<ICourseBase> Courses { get; }
        IEnumerable<ISchoolBase> Schools { get; }
    }
}
