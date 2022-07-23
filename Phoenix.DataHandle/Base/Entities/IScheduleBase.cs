namespace Phoenix.DataHandle.Base.Entities
{
    public interface IScheduleBase
    {
        DayOfWeek DayOfWeek { get; }
        DateTime StartTime { get; }
        DateTime EndTime { get; }
        string? Comments { get; }
    }
}