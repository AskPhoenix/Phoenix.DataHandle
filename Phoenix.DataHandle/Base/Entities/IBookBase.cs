namespace Phoenix.DataHandle.Base.Entities
{
    public interface IBookBase
    {
        string Name { get; }
        string? Publisher { get; }
        string? Comments { get; }
    }
}