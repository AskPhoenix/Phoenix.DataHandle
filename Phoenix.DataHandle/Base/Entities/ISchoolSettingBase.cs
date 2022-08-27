namespace Phoenix.DataHandle.Base.Entities
{
    public interface ISchoolSettingBase
    {
        string Country { get; }
        string PrimaryLanguage { get; }
        string PrimaryLocale { get; }
        string SecondaryLanguage { get; }
        string SecondaryLocale { get; }
        string TimeZone { get; }
        string PhoneCountryCode { get; }
    }
}
