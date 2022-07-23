namespace Phoenix.DataHandle.Base
{
    public interface ISchoolBase
    {
        int Code { get; }
        string Name { get; }
        string Slug { get; }
        string City { get; }
        string AddressLine { get; }
        string? Description { get; }
    }
}