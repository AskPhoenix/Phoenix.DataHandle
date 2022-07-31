namespace Phoenix.DataHandle.Base.Entities
{
    public interface IApplicationUserBase
    {
        string UserName { get; }
        string? Email { get; }
        string PhoneNumber { get; }
    }
}
