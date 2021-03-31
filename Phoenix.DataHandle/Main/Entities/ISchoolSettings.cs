namespace Phoenix.DataHandle.Main.Entities
{
    public interface ISchoolSettings
    {
        ISchool School { get; }
        string Language { get; set; }
        string Locale2 { get; set; }
        string TimeZone { get; set; }
    }
}
