using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Entities;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Base.Entities;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Api.Models
{
    public class SchoolApi : ISchoolApi, IModelApi
    {
        [JsonConstructor]
        public SchoolApi(int id, int code, string name, string slug, string city, string address,
            string? description, SchoolSettingApi schoolSetting)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(slug))
                throw new ArgumentNullException(nameof(slug));
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentNullException(nameof(city));
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentNullException(nameof(address));
            if (schoolSetting is null)
                throw new ArgumentNullException(nameof(schoolSetting));

            this.Id = id;
            this.Code = code;
            this.Name = name;
            this.Slug = slug;
            this.City = city;
            this.AddressLine = address;
            this.Description = description;
            this.SchoolSetting = schoolSetting;
        }

        public SchoolApi(int id, ISchoolSettingBase schoolSetting, ISchoolBase school)
            : this(id, school.Code, school.Name, school.Slug, school.City, school.AddressLine,
                  school.Description, new(id, schoolSetting))
        {
        }

        public SchoolApi(School school)
            : this(school.Id, school.SchoolSetting, school)
        {
        }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; }

        [JsonProperty(PropertyName = "code")]
        public int Code { get; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; } = null!;

        [JsonProperty(PropertyName = "slug")]
        public string Slug { get; } = null!;

        [JsonProperty(PropertyName = "city")]
        public string City { get; } = null!;

        [JsonProperty(PropertyName = "address")]
        public string AddressLine { get; } = null!;

        [JsonProperty(PropertyName = "description")]
        public string? Description { get; }

        [JsonProperty(PropertyName = "school_settings")]
        public SchoolSettingApi SchoolSetting { get; } = null!;

        ISchoolSettingApi ISchoolApi.SchoolSetting => this.SchoolSetting;
    }
}
