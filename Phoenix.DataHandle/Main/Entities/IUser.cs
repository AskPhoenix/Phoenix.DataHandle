namespace Phoenix.DataHandle.Main.Entities
{
    public interface IUser
    {
        IAspNetUser AspNetUser { get; }
        string FirstName { get; }
        string LastName { get; }
        string FullName => FirstName + " " + LastName;
        bool IsSelfDetermined { get; }
    }
}