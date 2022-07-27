namespace Phoenix.DataHandle.Base.Entities
{
    public interface IConnectionBase
    {
        string Channel { get; }
        string ChannelKey { get; }
        string ChannelDisplayName { get; }
        DateTime? ActivatedAt { get; }
        bool IsActive => ActivatedAt.HasValue;
    }
}
