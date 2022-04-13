namespace Phoenix.DataHandle.Main.Entities
{
    public interface IUserInfo
    {
        IUser User { get; }
        string FirstName { get; }
        string LastName { get; }
        string FullName { get; }
        bool IsSelfDetermined { get; }
    }
}