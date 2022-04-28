using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Api.Models.Main
{
    public class SchoolSettingApi : ISchoolSetting, IModelApi
    {
        [JsonConstructor]
        public SchoolSettingApi(int id, string country, string primaryLanguage, string primaryLocale,
            string secondaryLanguage, string secondaryLocale, string timeZone, string phoneCode)
        {
            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentNullException(nameof(country));
            if (string.IsNullOrWhiteSpace(primaryLanguage))
                throw new ArgumentNullException(nameof(primaryLanguage));
            if (string.IsNullOrWhiteSpace(primaryLocale))
                throw new ArgumentNullException(nameof(primaryLocale));
            if (string.IsNullOrWhiteSpace(secondaryLanguage))
                throw new ArgumentNullException(nameof(secondaryLanguage));
            if (string.IsNullOrWhiteSpace(secondaryLocale))
                throw new ArgumentNullException(nameof(secondaryLocale));
            if (string.IsNullOrWhiteSpace(timeZone))
                throw new ArgumentNullException(nameof(timeZone));
            if (string.IsNullOrWhiteSpace(phoneCode))
                throw new ArgumentNullException(nameof(phoneCode));

            this.Id = id;
            this.Country = country;
            this.PrimaryLanguage = primaryLanguage;
            this.PrimaryLocale = primaryLocale;
            this.SecondaryLanguage = secondaryLanguage;
            this.SecondaryLocale = secondaryLocale;
            this.TimeZone = timeZone;
            this.PhoneCountryCode = phoneCode;
        }

        public SchoolSettingApi(ISchoolSetting schoolSetting)
            : this(0, schoolSetting.Country, schoolSetting.PrimaryLanguage, schoolSetting.PrimaryLocale,
                  schoolSetting.SecondaryLanguage, schoolSetting.SecondaryLocale, schoolSetting.TimeZone, schoolSetting.PhoneCountryCode)
        {
            if (schoolSetting is SchoolSetting schoolSetting1)
                this.Id = schoolSetting1.SchoolId;
        }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; } = null!;

        [JsonProperty(PropertyName = "primary_language")]
        public string PrimaryLanguage { get; } = null!;

        [JsonProperty(PropertyName = "primary_locale")]
        public string PrimaryLocale { get; } = null!;

        [JsonProperty(PropertyName = "secondary_language")]
        public string SecondaryLanguage { get; } = null!;

        [JsonProperty(PropertyName = "secondary_locale")]
        public string SecondaryLocale { get; } = null!;

        [JsonProperty(PropertyName = "time_zone")]
        public string TimeZone { get; } = null!;

        [JsonProperty(PropertyName = "phone_code")]
        public string PhoneCountryCode { get; } = null!;

        [JsonIgnore]
        public ISchool School { get; } = null!;
    }
}
