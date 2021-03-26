using Newtonsoft.Json;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Utilities;
using Phoenix.DataHandle.WordPress.Models.Uniques;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Phoenix.DataHandle.WordPress.Models
{
    public class CourseACF : IModelACF<Course>
    {
        [JsonProperty(PropertyName = "code")]
        public short Code { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "subcourse")]
        public string SubCourse { get => subCourse; set => subCourse = string.IsNullOrWhiteSpace(value) ? null : value; }
        private string subCourse;

        [JsonProperty(PropertyName = "level")]
        public string Level { get; set; }

        [JsonProperty(PropertyName = "group")]
        public string Group { get; set; }

        [JsonProperty(PropertyName = "books")]
        public string BooksString { get; set; }

        [JsonProperty(PropertyName = "first_date")]
        private string FirstDateString { get; set; }
        public DateTimeOffset FirstDate { get => CalendarExtensions.GetDateTimeOffsetFromString(FirstDateString, "d"); }

        [JsonProperty(PropertyName = "last_date")]
        private string LastDateString { get; set; }
        public DateTimeOffset LastDate { get => CalendarExtensions.GetDateTimeOffsetFromString(LastDateString, "d"); }

        [JsonProperty(PropertyName = "comments")]
        public string Comments { get => comments; set => comments = string.IsNullOrWhiteSpace(value) ? null : value; }
        private string comments;

        public Expression<Func<Course, bool>> MatchesUnique => c =>
            c.School.Name == this.SchoolUnique.NormalizedSchoolName &&
            c.School.City == this.SchoolUnique.NormalizedSchoolCity &&
            c.Code == this.Code;

        public SchoolUnique SchoolUnique { get; set; }

        [JsonConstructor]
        public CourseACF(short code)
        {
            this.Code = code;
        }

        public CourseACF(CourseUnique courseUnique)
            : this(courseUnique.Code)
        {
            this.SchoolUnique = courseUnique.SchoolUnique;
        }

        public CourseACF(CourseACF other)
        {
            if (other is null)
                throw new ArgumentNullException(nameof(other));

            this.Code = other.Code;
            this.Name = other.Name;
            this.SubCourse = other.SubCourse;
            this.Level = other.Level;
            this.Group = other.Group;
            this.BooksString = other.BooksString;
            this.FirstDateString = other.FirstDateString;
            this.LastDateString = other.LastDateString;
            this.Comments = other.Comments;
        }

        public Course ToContext()
        {
            return new Course()
            {
                Code = this.Code,
                Name = this.Name.Truncate(150),
                SubCourse = this.SubCourse.Truncate(150),
                Level = this.Level.Truncate(50),
                Group = this.Group.Truncate(50),
                FirstDate = this.FirstDate,
                LastDate = this.LastDate,
                Info = this.Comments
            };
        }

        public IModelACF<Course> WithTitleCase()
        {
            return new CourseACF(this)
            {
                Name = this.Name.ToTitleCase(),
                SubCourse = this.SubCourse.ToTitleCase(),
                Level = this.Level.ToTitleCase(),
                BooksString = this.BooksString.ToTitleCase()
            };
        }

        public IEnumerable<Book> ExtractBooks()
        {
            if (string.IsNullOrEmpty(this.BooksString))
                return Enumerable.Empty<Book>();

            return this.BooksString.
                Split(',').
                Select(b => b.Trim()).
                Distinct().
                Select(b => new Book()
                {
                    Name = b.Truncate(255),
                    NormalizedName = b.ToUpperInvariant().Truncate(255),
                    CreatedAt = DateTimeOffset.UtcNow
                });
        }
    }
}
