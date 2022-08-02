using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Entities;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Base.Entities;
using Phoenix.DataHandle.Main.Models;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Phoenix.DataHandle.Api.Models
{
    public class SchoolSettingApi : ISchoolSettingApi, IModelApi
    {
        [JsonConstructor]
        public SchoolSettingApi(string country, string primaryLocale,
            string secondaryLocale, string timeZone, string phoneCountryCode)
        {
            if (string.IsNullOrWhiteSpace(country))
                country = null!;
            if (string.IsNullOrWhiteSpace(timeZone))
                timeZone = null!;
            
            this.SchoolId = 0;
            this.Country = country;
            this.PhoneCountryCode = phoneCountryCode;

            try
            {
                this.PrimaryLanguage = CultureInfo.GetCultureInfo(primaryLocale).EnglishName;
                this.PrimaryLocale = primaryLocale;
            }
            catch (CultureNotFoundException) { }

            try
            {
                this.SecondaryLanguage = CultureInfo.GetCultureInfo(secondaryLocale).EnglishName;
                this.SecondaryLocale = secondaryLocale;
            }
            catch (CultureNotFoundException) { }

            try
            {
                TimeZoneInfo.FindSystemTimeZoneById(timeZone);
                this.TimeZone = timeZone;
            }
            catch (TimeZoneNotFoundException) { }
        }

        public SchoolSettingApi(ISchoolSettingBase schoolSetting)
            : this(schoolSetting.Country, schoolSetting.PrimaryLocale,
                  schoolSetting.SecondaryLocale, schoolSetting.TimeZone, schoolSetting.PhoneCountryCode)
        {
        }

        public SchoolSettingApi(SchoolSetting schoolSetting)
            : this((ISchoolSettingApi)schoolSetting)
        {
            this.SchoolId = schoolSetting.SchoolId;
        }

        public SchoolSetting ToSchoolSetting()
        {
            return new SchoolSetting()
            {
                SchoolId = this.SchoolId,
                Country = this.Country,
                PrimaryLanguage = this.PrimaryLanguage,
                PrimaryLocale = this.PrimaryLocale,
                SecondaryLanguage = this.SecondaryLanguage,
                SecondaryLocale = this.SecondaryLocale,
                TimeZone = this.TimeZone,
                PhoneCountryCode = this.PhoneCountryCode
            };
        }

        public SchoolSetting ToSchoolSetting(SchoolSetting schoolSettingToUpdate)
        {
            schoolSettingToUpdate.Country = this.Country;
            schoolSettingToUpdate.PrimaryLanguage = this.PrimaryLanguage;
            schoolSettingToUpdate.PrimaryLocale = this.PrimaryLocale;
            schoolSettingToUpdate.SecondaryLanguage = this.SecondaryLanguage;
            schoolSettingToUpdate.SecondaryLocale = this.SecondaryLocale;
            schoolSettingToUpdate.TimeZone = this.TimeZone;
            schoolSettingToUpdate.PhoneCountryCode = this.PhoneCountryCode;

            return schoolSettingToUpdate;
        }

        [JsonProperty("school_id")]
        public int SchoolId { get; }

        [JsonIgnore]
        public int Id => this.SchoolId;

        [JsonProperty("country", Required = Required.Always)]
        public string Country { get; }

        [JsonProperty("primary_language")]
        public string PrimaryLanguage { get; } = null!;

        [RegularExpression("^[a-z]{2}-[A-Z]{2}$")]
        [JsonProperty("primary_locale", Required = Required.Always)]
        public string PrimaryLocale { get; } = null!;

        [JsonProperty("secondary_language")]
        public string SecondaryLanguage { get; } = null!;

        [RegularExpression("^[a-z]{2}-[A-Z]{2}$")]
        [JsonProperty("secondary_locale", Required = Required.Always)]
        public string SecondaryLocale { get; } = null!;

        [JsonProperty("time_zone", Required = Required.Always)]
        public string TimeZone { get; } = null!;

        [RegularExpression("^\\+\\d{1,3}$")]
        [JsonProperty("phone_country_code", Required = Required.Always)]
        public string PhoneCountryCode { get; }
    }
}
