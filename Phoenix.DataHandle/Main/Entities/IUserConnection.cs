namespace Phoenix.DataHandle.Main.Entities
{
    public interface IUserConnection
    {
        IUserInfo Tenant { get; }
        string Channel { get; }
        string ChannelKey { get; }
        string ChannelDisplayName { get; }
    }
}
