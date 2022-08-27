namespace Phoenix.DataHandle.Base.Entities
{
    public interface IExerciseBase
    {
        string Name { get; }
        string? Page { get; }
        string? Comments { get; }
    }
}