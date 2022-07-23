namespace Phoenix.DataHandle.Base
{
    public interface ICourseBase
    {
        short Code { get; }
        string Name { get; }
        string? SubCourse { get; }
        string Level { get; }
        string Group { get; }
        string? Comments { get; }
        DateTime FirstDate { get; }
        DateTime LastDate { get; }
    }
}