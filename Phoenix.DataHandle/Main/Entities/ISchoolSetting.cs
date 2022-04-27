namespace Phoenix.DataHandle.Main.Entities
{
    public interface ISchoolSetting
    {
        ISchool School { get; }
        string Country { get; }
        string PrimaryLanguage { get; }
        string PrimaryLocale { get; }
        string SecondaryLanguage { get; }
        string SecondaryLocale { get; }
        string TimeZone { get; }
        string PhoneCountryCode { get; }
    }
}
