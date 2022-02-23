namespace Phoenix.DataHandle.Main.Entities
{
    public interface IAspNetUserLogin
    {
        IAspNetUser User { get; }
        IChannel Channel { get; }
        string ProviderKey { get; }
        bool IsActive { get; }
    }
}
