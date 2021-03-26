using Newtonsoft.Json;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Utilities;
using Phoenix.DataHandle.WordPress.Models.Uniques;
using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

namespace Phoenix.DataHandle.WordPress.Models
{
    public class SchoolACF : IModelACF<School>
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "slug")]
        public string Slug { get => slug; set => slug = string.IsNullOrWhiteSpace(value) ? null : value; }
        private string slug;

        [JsonProperty(PropertyName = "language")]
        public string Language { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }

        [JsonProperty(PropertyName = "comments")]
        public string Comments { get => comments; set => comments = string.IsNullOrWhiteSpace(value) ? null : value; }
        private string comments;

        public string Locale => CultureInfo.GetCultures(CultureTypes.NeutralCultures).
                FirstOrDefault(c => c.EnglishName.ToUpperInvariant() == this.Language.ToUpperInvariant())?.
                TwoLetterISOLanguageName;

        public Expression<Func<School, bool>> MatchesUnique => s =>
            s.NormalizedName == this.SchoolUnique.NormalizedSchoolName &&
            s.NormalizedCity == this.SchoolUnique.NormalizedSchoolCity;

        public SchoolUnique SchoolUnique { get; set; }

        [JsonConstructor]
        public SchoolACF(string name, string city)
        {
            this.Name = name;
            this.City = city;

            this.SchoolUnique = new SchoolUnique(name, city);
        }

        public SchoolACF(SchoolUnique schoolUnique)
        {
            this.SchoolUnique = schoolUnique;
        }

        public SchoolACF(SchoolACF other)
        {
            if (other is null)
                throw new ArgumentNullException(nameof(other));

            this.Name = other.Name;
            this.Slug = other.Slug;
            this.City = other.City;
            this.Language = other.Language;
            this.Address = other.Address;
            this.Comments = other.comments;
            this.SchoolUnique = other.SchoolUnique;
        }

        public School ToContext()
        {
            return new School
            {
                Name = this.Name.Truncate(200),
                NormalizedName = this.Name.ToUpperInvariant().Truncate(200),
                Slug = (this.Slug ?? this.Name).Truncate(64),
                City = this.City.Truncate(200),
                NormalizedCity = this.City.Truncate(200),
                AddressLine = this.Address.Truncate(255),
                FacebookPageId = null,
                Info = this.Comments
            };
        }

        public IModelACF<School> WithTitleCase()
        {
            return new SchoolACF(this)
            {
                City = this.City.ToTitleCase(),
                Address = this.Address.ToTitleCase(),
            };
        }

        public SchoolSettings ExtractSchoolSettings()
        {
            return new SchoolSettings
            {
                Language = this.Language,
                TimeZone = string.Empty
            };
        }
    }
}
