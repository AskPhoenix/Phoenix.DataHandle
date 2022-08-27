using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Entities;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Base.Entities;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Api.Models
{
    public class CourseApi : ICourseApi, IModelApi
    {
        [JsonConstructor]
        public CourseApi(int id, short code, int schoolId, string name, string? subcourse,
            string level, string group, string? comments, DateTime firstDate, DateTime lastDate)
        {
            if (string.IsNullOrWhiteSpace(name))
                name = null!;
            if (string.IsNullOrWhiteSpace(level))
                level = null!;
            if (string.IsNullOrWhiteSpace(group))
                group = null!;

            this.Id = id;
            this.Code = code;
            this.SchoolId = schoolId;
            this.Name = name;
            this.SubCourse = subcourse;
            this.Level = level;
            this.Group = group;
            this.Comments = comments;
            this.FirstDate = firstDate.Date;
            this.LastDate = lastDate.Date;
        }

        public CourseApi(int id, int schoolId, ICourseBase course)
            : this(id, course.Code, schoolId, course.Name, course.SubCourse,
                  course.Level, course.Group, course.Comments, course.FirstDate, course.LastDate)
        {
        }

        public CourseApi(Course course)
            : this(course.Id, course.SchoolId, course)
        {
        }

        public Course ToCourse()
        {
            return new Course()
            {
                Id = this.Id,
                Code = this.Code,
                SchoolId = this.SchoolId,
                Name = this.Name,
                SubCourse = this.SubCourse,
                Level = this.Level,
                Group = this.Group,
                Comments = this.Comments,
                FirstDate = this.FirstDate,
                LastDate = this.LastDate
            };
        }

        public Course ToCourse(Course courseToUpdate)
        {
            courseToUpdate.Code = this.Code;
            courseToUpdate.Name = this.Name;
            courseToUpdate.SubCourse = this.SubCourse;
            courseToUpdate.Level = this.Level;
            courseToUpdate.Group = this.Group;
            courseToUpdate.Comments = this.Comments;
            courseToUpdate.FirstDate = this.FirstDate;
            courseToUpdate.LastDate = this.LastDate;

            return courseToUpdate;
        }

        [JsonProperty("id")]
        public int Id { get; }

        [JsonProperty("code")]
        public short Code { get; }

        [JsonProperty("school_id", Required = Required.Always)]
        public int SchoolId { get; }

        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; } = null!;

        [JsonProperty("subcourse")]
        public string? SubCourse { get; }

        [JsonProperty("level", Required = Required.Always)]
        public string Level { get; } = null!;

        [JsonProperty("group", Required = Required.Always)]
        public string Group { get; } = null!;

        [JsonProperty("comments")]
        public string? Comments { get; }

        [JsonProperty("first_date", Required = Required.Always)]
        public DateTime FirstDate { get; }

        [JsonProperty("last_date", Required = Required.Always)]
        public DateTime LastDate { get; }
    }
}
