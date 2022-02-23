namespace Phoenix.DataHandle.Main.Entities
{
    public interface IMaterial
    {
        IExam Exam { get; }
        IBook? Book { get; }
        string? Chapter { get; }
        string? Section { get; }
        string? Comments { get; }
    }
}
