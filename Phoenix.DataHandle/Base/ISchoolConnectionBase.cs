namespace Phoenix.DataHandle.Base
{
    public interface ISchoolConnectionBase
    {
        string Channel { get; }
        string ChannelKey { get; }
        string ChannelDisplayName { get; }
    }
}
