namespace Phoenix.DataHandle.Main.Entities
{
    public interface ISchoolLogin
    {
        ISchool School { get; }
        IChannel Channel { get; }
        string ProviderKey { get; }
    }
}
