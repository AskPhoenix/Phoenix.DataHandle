using Newtonsoft.Json;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Utilities;
using Phoenix.DataHandle.DataEntry.Models.Uniques;
using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Phoenix.DataHandle.DataEntry.Models.Extensions;

namespace Phoenix.DataHandle.DataEntry.Models
{
    public class SchoolACF : IModelACF
    {
        [JsonProperty(PropertyName = "code")]
        public int Code { get; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; }

        public string NormalizedName => this.SchoolUnique.NormalizedSchoolName;

        [JsonProperty(PropertyName = "slug")]
        public string? Slug { get; }

        [JsonProperty(PropertyName = "language")]
        public string Language { get; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; }

        public string NormalizedCity => this.SchoolUnique.NormalizedSchoolCity;

        [JsonProperty(PropertyName = "address")]
        public string Address { get; }

        [JsonProperty(PropertyName = "comments")]
        public string? Comments { get; }

        public string Locale { get; }

        public SchoolUnique SchoolUnique { get; }

        [JsonConstructor]
        public SchoolACF(string name, string? slug, string language, string city, string address, string? info)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(language))
                throw new ArgumentNullException(nameof(language));
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentNullException(nameof(city));
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentNullException(nameof(address));

            this.Name = name.Trim().Truncate(200);
            this.Slug = (string.IsNullOrWhiteSpace(slug) ? name : slug).Trim().Truncate(64);
            this.City = city.Trim().Truncate(200).ToTitleCase();
            this.Address = address.Trim().Truncate(255);
            this.Comments = string.IsNullOrWhiteSpace(info) ? null : info.Trim();
            this.Language = language;

            this.Locale = CultureInfo.GetCultures(CultureTypes.NeutralCultures).
                First(c => c.EnglishName.Equals(this.Language, StringComparison.InvariantCultureIgnoreCase)).
                TwoLetterISOLanguageName;

            this.SchoolUnique = new(this.Name, this.City);
        }

        public Expression<Func<School, bool>> GetUniqueExpression() => s => s.Code == this.Code;
    }
}
