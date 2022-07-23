namespace Phoenix.DataHandle.Base
{
    public interface IBookBase
    {
        string Name { get; }
        string? Publisher { get; }
        string? Comments { get; }
    }
}