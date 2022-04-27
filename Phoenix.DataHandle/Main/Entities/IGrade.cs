namespace Phoenix.DataHandle.Main.Entities
{
    public interface IGrade
    {
        IUserInfo Student { get; }
        ICourse? Course { get; }
        IExam? Exam { get; }
        IExercise? Exercise { get; }
        decimal Score { get; }
        string? Topic { get; }
        string? Justification { get; }
    }
}
