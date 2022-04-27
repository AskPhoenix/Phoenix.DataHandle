using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Api.Models.Main
{
    public class SchoolApi : ISchool, IModelApi
    {
        private SchoolApi()
        {
            this.Classrooms = new List<ClassroomApi>();
            this.Courses = new List<CourseApi>();

            this.Broadcasts = new List<IBroadcast>();
            this.SchoolConnections = new List<ISchoolConnection>();
            this.Users = new List<IUser>();
        }

        [JsonConstructor]
        public SchoolApi(int id, int code, string name, string slug, string city, string address, string? description, 
            SchoolSettingApi schoolSetting, List<ClassroomApi>? classrooms, List<CourseApi>? courses)
            : this()
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));
            if (slug is null)
                throw new ArgumentNullException(nameof(slug));
            if (city is null)
                throw new ArgumentNullException(nameof(city));
            if (address is null)
                throw new ArgumentNullException(nameof(address));

            this.Id = id;
            this.Code = code;
            this.Name = name;
            this.Slug = slug;
            this.City = city;
            this.AddressLine = address;
            this.Description = description;
            this.SchoolSetting = schoolSetting;

            if (classrooms is not null)
                this.Classrooms = Classrooms;
            if (courses is not null)
                this.Courses = courses;
        }

        public SchoolApi(ISchool school, bool include = false)
            : this(0, school.Code, school.Name, school.Slug, school.City, school.AddressLine, school.Description, 
                  null!, null, null)
        {
            if (school is School school1)
                this.Id = school1.Id;

            if (school.SchoolSetting is not null)
                this.SchoolSetting = new SchoolSettingApi(school.SchoolSetting);

            // SchoolSetting is always included if it's not null
            if (!include)
                return;

            this.Classrooms = school.Classrooms.Select(c => new ClassroomApi(c)).ToList();
            this.Courses = school.Courses.Select(c => new CourseApi(c)).ToList();
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

        [JsonProperty(PropertyName = "school_info")]
        public SchoolSettingApi SchoolSetting { get; } = null!;

        [JsonProperty(PropertyName = "classrooms")]
        public List<ClassroomApi> Classrooms { get; }

        [JsonProperty(PropertyName = "courses")]
        public List<CourseApi> Courses { get; }


        ISchoolSetting ISchool.SchoolSetting => this.SchoolSetting;

        [JsonIgnore]
        public IEnumerable<IBroadcast> Broadcasts { get; }

        IEnumerable<IClassroom> ISchool.Classrooms => this.Classrooms;
        
        IEnumerable<ICourse> ISchool.Courses => this.Courses;

        [JsonIgnore]
        public IEnumerable<ISchoolConnection> SchoolConnections { get; }

        [JsonIgnore]
        public IEnumerable<IUser> Users { get; }
    }
}
