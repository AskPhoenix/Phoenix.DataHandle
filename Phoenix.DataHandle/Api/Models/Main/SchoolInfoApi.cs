using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Main.Entities;
using System;

namespace Phoenix.DataHandle.Api.Models.Main
{
    public class SchoolInfoApi : ISchoolInfo, IModelApi
    {
        [JsonConstructor]
        public SchoolInfoApi(SchoolApi school, string country, string primaryLanguage, string primaryLocale,
            string secondaryLanguage, string secondaryLocale, string timeZone, string phoneCode)
        {
            if (school is null)
                throw new ArgumentNullException(nameof(school));
            if (country is null)
                throw new ArgumentNullException(nameof(country));
            if (primaryLanguage is null)
                throw new ArgumentNullException(nameof(primaryLanguage));
            if (primaryLocale is null)
                throw new ArgumentNullException(nameof(primaryLocale));
            if (secondaryLanguage is null)
                throw new ArgumentNullException(nameof(secondaryLanguage));
            if (secondaryLocale is null)
                throw new ArgumentNullException(nameof(secondaryLocale));
            if (timeZone is null)
                throw new ArgumentNullException(nameof(timeZone));
            if (phoneCode is null)
                throw new ArgumentNullException(nameof(phoneCode));

            this.School = school;
            this.Country = country;
            this.PrimaryLanguage = primaryLanguage;
            this.PrimaryLocale = primaryLocale;
            this.SecondaryLanguage = secondaryLanguage;
            this.SecondaryLocale = secondaryLocale;
            this.TimeZone = timeZone;
            this.PhoneCode = phoneCode;
        }

        public SchoolInfoApi(ISchoolInfo schoolInfo)
            : this(new SchoolApi(schoolInfo.School), schoolInfo.Country, schoolInfo.PrimaryLanguage, schoolInfo.PrimaryLocale,
                  schoolInfo.SecondaryLanguage, schoolInfo.SecondaryLocale, schoolInfo.TimeZone, schoolInfo.PhoneCode)
        {
        }


        [JsonProperty(PropertyName = "school")]
        public SchoolApi School { get; } = null!;

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
        public string PhoneCode { get; } = null!;

        
        int IModelApi.Id => this.School.Id;

        ISchool ISchoolInfo.School => this.School;
    }
}
