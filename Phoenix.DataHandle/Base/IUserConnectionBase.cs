namespace Phoenix.DataHandle.Base
{
    public interface IUserConnectionBase
    {
        string Channel { get; }
        string ChannelKey { get; }
        string ChannelDisplayName { get; }
    }
}
