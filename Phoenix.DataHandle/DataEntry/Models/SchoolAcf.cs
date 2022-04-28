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
        public SchoolAcf(int? code, string name, string? slug, string city, string address, string? comments, string language)
            : this()
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentNullException(nameof(city));
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentNullException(nameof(address));
            
            if (string.IsNullOrWhiteSpace(language))
                language = "English";

            this.Code = code ?? 0;
            this.Name = name.Trim();
            this.Slug = (string.IsNullOrWhiteSpace(slug) ? name : slug).Trim();
            this.City = city.Trim().ToTitleCase();
            this.AddressLine = address.Trim();
            this.Description = string.IsNullOrWhiteSpace(comments) ? null : comments.Trim();

            var locale = CultureInfo.GetCultures(CultureTypes.NeutralCultures).
                First(c => c.EnglishName.Equals(language, StringComparison.InvariantCultureIgnoreCase)).
                TwoLetterISOLanguageName;

            this.SchoolSetting = new SchoolSetting
            {
                PrimaryLanguage = language,
                PrimaryLocale = locale
            };

            this.BotLanguage = this.SchoolSetting.PrimaryLanguage;
        }

        public SchoolUnique GetSchoolUnique() => new(this.Code);

        [JsonIgnore]
        public int Code { get; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; } = null!;

        [JsonProperty(PropertyName = "slug")]
        public string Slug { get; } = null!;

        [JsonProperty(PropertyName = "language")]
        public string BotLanguage { get; } = null!;

        [JsonProperty(PropertyName = "city")]
        public string City { get; } = null!;

        [JsonProperty(PropertyName = "address")]
        public string AddressLine { get; } = null!;

        [JsonProperty(PropertyName = "comments")]
        public string? Description { get; }

        [JsonIgnore]
        // TODO: Check in repository what happens with CreatedAt and other properties when creating/updating the object
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
