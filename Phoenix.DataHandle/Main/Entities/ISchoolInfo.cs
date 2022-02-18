namespace Phoenix.DataHandle.Main.Entities
{
    public interface ISchoolInfo
    {
        ISchool School { get; }
        string Country { get; set; }
        string PrimaryLanguage { get; set; }
        string PrimaryLocale { get; set; }
        string SecondaryLanguage { get; set; }
        string SecondaryLocale { get; set; }
        string TimeZone { get; set; }
        string PhoneCode { get; set; }
    }
}
