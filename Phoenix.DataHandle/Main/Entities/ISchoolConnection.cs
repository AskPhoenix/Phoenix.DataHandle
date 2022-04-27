namespace Phoenix.DataHandle.Main.Entities
{
    public interface ISchoolConnection
    {
        ISchool Tenant { get; }
        string Channel { get; }
        string ChannelKey { get; }
        string ChannelDisplayName { get; }
    }
}
