namespace Phoenix.DataHandle.Main.Entities
{
    public interface IGrade
    {
        IAspNetUser Student { get; }
        ICourse? Course { get; }
        IExam? Exam { get; }
        IExercise? Exercise { get; }
        decimal Score { get; set; }
        string? Topic { get; set; }
        string? Justification { get; set; }
    }
}
