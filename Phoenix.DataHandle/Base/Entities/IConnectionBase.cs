namespace Phoenix.DataHandle.Base.Entities
{
    public interface IConnectionBase
    {
        string Channel { get; }
        string ChannelKey { get; }
        DateTime? ActivatedAt { get; }
    }
}
