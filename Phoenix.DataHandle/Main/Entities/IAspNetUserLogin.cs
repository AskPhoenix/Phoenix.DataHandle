namespace Phoenix.DataHandle.Main.Entities
{
    public interface IAspNetUserLogin
    {
        IAspNetUser User { get; }
        IChannel Channel { get; }
        string ProviderKey { get; set; }
        bool IsActive { get; set; }
    }
}
