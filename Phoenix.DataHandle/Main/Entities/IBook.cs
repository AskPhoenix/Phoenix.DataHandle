namespace Phoenix.DataHandle.Main.Entities
{
    public interface IBook
    {
        string Name { get; }
        string? Publisher { get; }
        string? Comments { get; }

        IEnumerable<IExercise> Exercises { get; }
        IEnumerable<IMaterial> Materials { get; }
        
        IEnumerable<ICourse> Courses { get; }
    }
}