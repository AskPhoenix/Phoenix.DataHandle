namespace Phoenix.DataHandle.Main.Entities
{
    public interface IMaterial
    {
        IExam Exam { get; }
        IBook Book { get; }
        string Chapter { get; set; }
        string Section { get; set; }
        string Comments { get; set; }
    }
}
