namespace Phoenix.DataHandle.Main.Entities
{
    public interface IUser
    {
        IAspNetUser AspNetUser { get; }
        string? FirstName { get; }
        string? LastName { get; }
        string? FullName { get; }
        bool IsSelfDetermined { get; }
    }
}