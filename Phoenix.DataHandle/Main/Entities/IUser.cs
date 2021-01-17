namespace Phoenix.DataHandle.Main.Entities
{
    public interface IUser
    {
        IAspNetUsers AspNetUser { get; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string FullName { get; }
        bool TermsAccepted { get; set; }
        bool IsSelfDetermined { get; set; }
    }
}