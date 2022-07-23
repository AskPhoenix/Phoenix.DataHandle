namespace Phoenix.DataHandle.Base
{
    public interface IAspNetUserBase
    {
        string UserName { get; }
        string? Email { get; }
        string PhoneNumber { get; }
    }
}
