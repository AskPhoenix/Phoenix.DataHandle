namespace Phoenix.DataHandle.Base.Entities
{
    public interface IGradeBase
    {
        decimal Score { get; }
        string? Topic { get; }
        string? Justification { get; }
    }
}
