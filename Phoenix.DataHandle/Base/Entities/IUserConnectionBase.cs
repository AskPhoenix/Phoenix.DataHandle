namespace Phoenix.DataHandle.Base.Entities
{
    public interface IUserConnectionBase
    {
        string Channel { get; }
        string ChannelKey { get; }
        string ChannelDisplayName { get; }
    }
}
