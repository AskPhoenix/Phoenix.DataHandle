using Phoenix.DataHandle.Base.Entities;

namespace Phoenix.DataHandle.DataEntry.Entities
{
    public interface IScheduleAcf : IScheduleBase
    {
        IClassroomBase? Classroom { get; }
    }
}
