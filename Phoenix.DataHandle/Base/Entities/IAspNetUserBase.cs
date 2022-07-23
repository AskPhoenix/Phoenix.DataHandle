namespace Phoenix.DataHandle.Base.Entities
{
    public interface IAspNetUserBase
    {
        string UserName { get; }
        string? Email { get; }
        string PhoneNumber { get; }
    }
}
