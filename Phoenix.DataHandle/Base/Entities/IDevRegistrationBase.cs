namespace Phoenix.DataHandle.Base.Entities
{
    public interface IDevRegistrationBase
    {
        string Email { get; set; }
        string RegisterKey { get; set; }
        DateTime? RegisteredAt { get; set; }
    }
}
