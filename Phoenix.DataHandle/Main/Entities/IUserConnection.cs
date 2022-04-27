namespace Phoenix.DataHandle.Main.Entities
{
    public interface IUserConnection
    {
        IUser Tenant { get; }
        string Channel { get; }
        string ChannelKey { get; }
        string ChannelDisplayName { get; }
    }
}
