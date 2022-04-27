namespace Phoenix.DataHandle.Main.Entities
{
    public interface IAspNetUser
    {
        string UserName { get; }
        string? Email { get; }
        string PhoneNumber { get; }

        IUser User { get; }
    }
}
