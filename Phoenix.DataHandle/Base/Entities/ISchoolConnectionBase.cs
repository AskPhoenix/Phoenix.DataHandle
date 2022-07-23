namespace Phoenix.DataHandle.Base.Entities
{
    public interface ISchoolConnectionBase
    {
        string Channel { get; }
        string ChannelKey { get; }
        string ChannelDisplayName { get; }
    }
}
