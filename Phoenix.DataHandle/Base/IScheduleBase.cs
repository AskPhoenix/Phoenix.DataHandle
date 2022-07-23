namespace Phoenix.DataHandle.Base
{
    public interface IScheduleBase
    {
        DayOfWeek DayOfWeek { get; }
        DateTime StartTime { get; }
        DateTime EndTime { get; }
        string? Comments { get; }
    }
}