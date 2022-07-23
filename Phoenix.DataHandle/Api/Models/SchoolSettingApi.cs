using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Entities;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Base.Entities;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Api.Models
{
    public class SchoolSettingApi : ISchoolSettingApi, IModelApi
    {
        [JsonConstructor]
        public SchoolSettingApi(int schoolId, string country, string primaryLanguage, string primaryLocale,
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

            this.SchoolId = schoolId;
            this.Country = country;
            this.PrimaryLanguage = primaryLanguage;
            this.PrimaryLocale = primaryLocale;
            this.SecondaryLanguage = secondaryLanguage;
            this.SecondaryLocale = secondaryLocale;
            this.TimeZone = timeZone;
            this.PhoneCountryCode = phoneCode;
        }

        public SchoolSettingApi(int schoolId, ISchoolSettingBase schoolSetting)
            : this(schoolId, schoolSetting.Country, schoolSetting.PrimaryLanguage, schoolSetting.PrimaryLocale,
                  schoolSetting.SecondaryLanguage, schoolSetting.SecondaryLocale, schoolSetting.TimeZone,
                  schoolSetting.PhoneCountryCode)
        {
        }

        public SchoolSettingApi(SchoolSetting schoolSetting)
            : this(schoolSetting.SchoolId, schoolSetting)
        {
        }

        [JsonProperty(PropertyName = "school_id")]
        public int SchoolId { get; }

        [JsonIgnore]
        public int Id => this.SchoolId;

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
    }
}
