namespace Phoenix.DataHandle.Base.Entities
{
    public interface IUserBase
    {
        string FirstName { get; }
        string LastName { get; }
        string FullName { get; }
        bool IsSelfDetermined { get; }
        int DependenceOrder { get; }
    }
}