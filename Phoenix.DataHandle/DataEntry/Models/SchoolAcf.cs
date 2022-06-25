using Newtonsoft.Json;
using Phoenix.DataHandle.DataEntry.Models.Extensions;
using Phoenix.DataHandle.DataEntry.Models.Uniques;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Utilities;
using System.Globalization;

namespace Phoenix.DataHandle.DataEntry.Models
{
    public class SchoolAcf : IModelAcf, ISchool
    {
        private SchoolAcf()
        {
            this.Classrooms = new List<IClassroom>();
            this.Courses = new List<ICourse>();
            this.Broadcasts = new List<IBroadcast>();
            this.SchoolConnections = new List<ISchoolConnection>();
            this.Users = new List<IUser>();
        }

        [JsonConstructor]
        public SchoolAcf(int? code, string name, string? slug, string city, string address, string? comments,
            string primary_language, string secondary_language, string timezone, string country,
            string phone_country_code)
            : this()
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentNullException(nameof(city));
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentNullException(nameof(address));
            if (string.IsNullOrWhiteSpace(primary_language))
                throw new ArgumentNullException(nameof(primary_language));
            if (string.IsNullOrWhiteSpace(secondary_language))
                throw new ArgumentNullException(nameof(secondary_language));
            if (string.IsNullOrWhiteSpace(timezone))
                throw new ArgumentNullException(nameof(timezone));
            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentNullException(nameof(country));
            if (string.IsNullOrWhiteSpace(phone_country_code))
                throw new ArgumentNullException(nameof(phone_country_code));

            this.Code = code ?? 0;
            this.Name = name.Trim();
            this.Slug = (string.IsNullOrWhiteSpace(slug) ? name : slug).Trim();
            this.City = city.Trim().ToTitleCase();
            this.AddressLine = address.Trim();
            this.Description = string.IsNullOrWhiteSpace(comments) ? null : comments.Trim();

            this.PrimaryLanguage = primary_language;
            this.SecondaryLanguage = secondary_language;
            this.TimeZone = timezone;
            this.Country = country;
            this.PhoneCountryCode = phone_country_code;

            this.PrimaryLocale = CultureInfo.GetCultures(CultureTypes.NeutralCultures).
                First(c => c.EnglishName.Equals(this.PrimaryLanguage, StringComparison.InvariantCultureIgnoreCase)).
                TwoLetterISOLanguageName;
            this.SecondaryLocale = CultureInfo.GetCultures(CultureTypes.NeutralCultures).
                First(c => c.EnglishName.Equals(this.SecondaryLanguage, StringComparison.InvariantCultureIgnoreCase)).
                TwoLetterISOLanguageName;
        }

        public School ToSchool()
        {
            return new()
            {
                Code = this.Code,
                Name = this.Name,
                Slug = this.Slug,
                City = this.City,
                AddressLine = this.AddressLine,
                Description = this.Description,

                SchoolSetting = new()
                {
                    Country = this.Country,
                    PrimaryLanguage = this.PrimaryLanguage,
                    PrimaryLocale = this.PrimaryLocale,
                    SecondaryLanguage = this.SecondaryLanguage,
                    SecondaryLocale = this.SecondaryLocale,
                    TimeZone = this.TimeZone,
                    PhoneCountryCode = this.PhoneCountryCode
                }
            };
        }

        public School ToSchool(School schoolFrom)
        {
            if (schoolFrom is null)
                throw new ArgumentNullException(nameof(schoolFrom));

            schoolFrom.Code = this.Code;
            schoolFrom.Name = this.Name;
            schoolFrom.Slug = this.Slug;
            schoolFrom.City = this.City;
            schoolFrom.AddressLine = this.AddressLine;
            schoolFrom.Description = this.Description;

            if (schoolFrom.SchoolSetting is not null)
            {
                schoolFrom.SchoolSetting.Country = this.Country;
                schoolFrom.SchoolSetting.PrimaryLanguage = this.PrimaryLanguage;
                schoolFrom.SchoolSetting.PrimaryLocale = this.PrimaryLocale;
                schoolFrom.SchoolSetting.SecondaryLanguage = this.SecondaryLanguage;
                schoolFrom.SchoolSetting.SecondaryLocale = this.SecondaryLocale;
                schoolFrom.SchoolSetting.TimeZone = this.TimeZone;
                schoolFrom.SchoolSetting.PhoneCountryCode = this.PhoneCountryCode;
            }

            return schoolFrom;
        }

        public SchoolUnique GetSchoolUnique() => new(this.Code);

        [JsonIgnore]
        public int Code { get; internal set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; } = null!;

        [JsonProperty(PropertyName = "slug")]
        public string Slug { get; } = null!;

        [JsonProperty(PropertyName = "primary_language")]
        public string PrimaryLanguage { get; } = null!;

        [JsonProperty(PropertyName = "city")]
        public string City { get; } = null!;

        [JsonProperty(PropertyName = "address")]
        public string AddressLine { get; } = null!;

        [JsonProperty(PropertyName = "comments")]
        public string? Description { get; }

        [JsonProperty(PropertyName = "secondary_language")]
        public string SecondaryLanguage { get; } = null!;

        [JsonProperty(PropertyName = "timezone")]
        public string TimeZone { get; } = null!;

        [JsonProperty(PropertyName = "country")]
        public string Country { get; } = null!;

        [JsonProperty(PropertyName = "phone_country_code")]
        public string PhoneCountryCode { get; } = null!;

        [JsonIgnore]
        public string PrimaryLocale { get; }

        [JsonIgnore]
        public string SecondaryLocale { get; }

        [JsonIgnore]
        public ISchoolSetting SchoolSetting { get; } = null!;


        [JsonIgnore]
        public IEnumerable<IBroadcast> Broadcasts { get; }

        [JsonIgnore]
        public IEnumerable<IClassroom> Classrooms { get; }

        [JsonIgnore]
        public IEnumerable<ICourse> Courses { get; }

        [JsonIgnore]
        public IEnumerable<ISchoolConnection> SchoolConnections { get; }

        [JsonIgnore]
        public IEnumerable<IUser> Users { get; }
    }
}
