namespace Phoenix.DataHandle.Base.Entities
{
    public interface ISchoolConnectionBase : IConnectionBase
    {
        string? ChannelToken { get; }
    }
}
