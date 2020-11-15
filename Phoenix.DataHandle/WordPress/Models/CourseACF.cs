using Newtonsoft.Json;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Utilities;
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

        public int SchoolId { get; set; }

        public Expression<Func<Course, bool>> MatchesUnique => c => c != null && c.SchoolId == this.SchoolId && c.Code == this.Code;

        public Course ToContext()
        {
            return new Course()
            {
                SchoolId = this.SchoolId,
                Code = this.Code,
                Name = this.Name?.Substring(0, Math.Min(this.Name.Length, 150)),
                SubCourse = this.SubCourse?.Substring(0, Math.Min(this.SubCourse.Length, 150)),
                Level = this.Level?.Substring(0, Math.Min(this.Level.Length, 50)),
                Group = this.Group?.Substring(0, Math.Min(this.Group.Length, 50)),
                FirstDate = this.FirstDate,
                LastDate = this.LastDate,
                Info = this.Comments,
                CreatedAt = DateTimeOffset.Now
            };
        }

        public IModelACF<Course> WithTitleCase()
        {
            return new CourseACF()
            {
                Code = this.Code,
                Name = this.Name?.UpperToTitleCase(),
                SubCourse = this.SubCourse?.UpperToTitleCase(),
                Level = this.Level?.UpperToTitleCase(),
                Group = this.Group,
                BooksString = this.BooksString?.UpperToTitleCase(),
                FirstDateString = this.FirstDateString,
                LastDateString = this.LastDateString,
                Comments = this.Comments,
                SchoolId = this.SchoolId
            };
        }

        public IEnumerable<Book> ExtractBooks()
        {
            if (string.IsNullOrEmpty(this.BooksString))
                return Enumerable.Empty<Book>();

            return this.BooksString.
                Split(',').
                Select(b => b.Trim()).
                Select(b => new Book()
                {
                    Name = b.Substring(0, Math.Min(b.Length, 255)),
                    CreatedAt = DateTimeOffset.Now
                });
        }
    }
}
