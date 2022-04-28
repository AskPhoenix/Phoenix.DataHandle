namespace Phoenix.DataHandle.Main.Entities
{
    public interface IExercise
    {
        ILecture Lecture { get; }
        string Name { get; }
        IBook? Book { get; }
        string? Page { get; }
        string? Comments { get; }

        IEnumerable<IGrade> Grades { get; }
    }
}