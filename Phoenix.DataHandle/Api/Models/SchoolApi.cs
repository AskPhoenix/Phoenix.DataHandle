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
        public SchoolApi(string name, string slug, string city, string addressLine,
            string? description, SchoolSettingApi schoolSetting)
        {
            if (string.IsNullOrWhiteSpace(name))
                name = null!;
            if (string.IsNullOrWhiteSpace(slug))
                slug = null!;
            if (string.IsNullOrWhiteSpace(city))
                city = null!;
            if (string.IsNullOrWhiteSpace(addressLine))
                addressLine = null!;
            if (schoolSetting is null)
                schoolSetting = null!;

            this.Id = 0;
            this.Code = 0;
            this.Name = name;
            this.Slug = slug;
            this.City = city;
            this.AddressLine = addressLine;
            this.Description = description;
            this.SchoolSetting = schoolSetting;
        }

        public SchoolApi(ISchoolSettingBase schoolSetting, ISchoolBase school)
            : this(school.Name, school.Slug, school.City, school.AddressLine,
                  school.Description, new(schoolSetting))
        {
            this.Code = school.Code;
        }

        public SchoolApi(School school)
            : this(school.SchoolSetting, school)
        {
            this.Id = school.Id;
        }

        public School ToSchool()
        {
            return new School()
            {
                Id = this.Id,
                Code = this.Code,
                Name = this.Name,
                Slug = this.Slug,
                City = this.City,
                AddressLine = this.AddressLine,
                Description = this.Description,

                SchoolSetting = this.SchoolSetting.ToSchoolSetting()
            };
        }

        public School ToSchool(School schoolToUpdate)
        {
            schoolToUpdate.Name = this.Name;
            schoolToUpdate.Slug = this.Slug;
            schoolToUpdate.City = this.City;
            schoolToUpdate.AddressLine = this.AddressLine;
            schoolToUpdate.Description = this.Description;

            if (schoolToUpdate.SchoolSetting is not null)
                schoolToUpdate.SchoolSetting = this.SchoolSetting.ToSchoolSetting(schoolToUpdate.SchoolSetting);

            return schoolToUpdate;
        }

        [JsonProperty("id")]
        public int Id { get; }

        [JsonProperty("code")]
        public int Code { get; }

        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; }

        [JsonProperty("slug", Required = Required.Always)]
        public string Slug { get; }

        [JsonProperty("city", Required = Required.Always)]
        public string City { get; }

        [JsonProperty("address", Required = Required.Always)]
        public string AddressLine { get; }

        [JsonProperty("description")]
        public string? Description { get; }

        [JsonProperty("school_settings", Required = Required.Always)]
        public SchoolSettingApi SchoolSetting { get; }

        ISchoolSettingApi ISchoolApi.SchoolSetting => this.SchoolSetting;
    }
}
