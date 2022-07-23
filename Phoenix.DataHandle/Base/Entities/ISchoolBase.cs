namespace Phoenix.DataHandle.Base.Entities
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