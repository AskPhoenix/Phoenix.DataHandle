namespace Phoenix.DataHandle.Base
{
    public interface IGradeBase
    {
        decimal Score { get; }
        string? Topic { get; }
        string? Justification { get; }
    }
}
