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
                throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(level))
                throw new ArgumentNullException(nameof(level));
            if (string.IsNullOrWhiteSpace(group))
                throw new ArgumentNullException(nameof(group));

            this.Id = id;
            this.Code = code;
            this.SchoolId = schoolId;
            this.Name = name;
            this.SubCourse = subcourse;
            this.Level = level;
            this.Group = group;
            this.Comments = comments;
            this.FirstDate = firstDate;
            this.LastDate = lastDate;
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

        [JsonProperty(PropertyName = "id")]
        public int Id { get; }

        [JsonProperty(PropertyName = "code")]
        public short Code { get; }

        [JsonProperty(PropertyName = "school_id")]
        public int SchoolId { get; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; } = null!;

        [JsonProperty(PropertyName = "subcourse")]
        public string? SubCourse { get; }

        [JsonProperty(PropertyName = "level")]
        public string Level { get; } = null!;

        [JsonProperty(PropertyName = "group")]
        public string Group { get; } = null!;

        [JsonProperty(PropertyName = "comments")]
        public string? Comments { get; }

        [JsonProperty(PropertyName = "first_date")]
        public DateTime FirstDate { get; }

        [JsonProperty(PropertyName = "last_date")]
        public DateTime LastDate { get; }
    }
}
