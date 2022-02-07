using Newtonsoft.Json;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Utilities;
using Phoenix.DataHandle.DataEntry.Models.Uniques;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Phoenix.DataHandle.DataEntry.Models.Extensions;

namespace Phoenix.DataHandle.DataEntry.Models
{
    public class CourseACF : IModelACF
    {
        [JsonProperty(PropertyName = "code")]
        public short Code { get; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; }

        [JsonProperty(PropertyName = "subcourse")]
        public string? SubCourse { get; }

        [JsonProperty(PropertyName = "level")]
        public string Level { get; }

        [JsonProperty(PropertyName = "group")]
        public string Group { get; }

        [JsonProperty(PropertyName = "books")]
        public string BooksString { get; }

        [JsonProperty(PropertyName = "first_date")]
        private string FirstDateString { get; }

        [JsonProperty(PropertyName = "last_date")]
        private string LastDateString { get; }

        [JsonProperty(PropertyName = "comments")]
        public string? Comments { get; }

        public List<Book> Books { get; }

        [JsonConstructor]
        public CourseACF(short code, string name, string? subcourse, string level, string group, string booksString, 
            string firstDateString, string lastdateString, string? comments)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(level))
                throw new ArgumentNullException(nameof(level));
            if (string.IsNullOrWhiteSpace(group))
                throw new ArgumentNullException(nameof(group));
            if (string.IsNullOrWhiteSpace(booksString))
                throw new ArgumentNullException(nameof(booksString));
            if (string.IsNullOrWhiteSpace(firstDateString))
                throw new ArgumentNullException(nameof(firstDateString));
            if (string.IsNullOrWhiteSpace(lastdateString))
                throw new ArgumentNullException(nameof(lastdateString));

            this.Code = code;
            this.Name = name.Trim().Truncate(150).ToTitleCase();
            this.SubCourse = string.IsNullOrWhiteSpace(subcourse) ? null : subcourse.Trim().Truncate(150).ToTitleCase();
            this.Level = level.Trim().Truncate(50).ToTitleCase();
            this.Group = group.Trim().Truncate(50);
            this.BooksString = booksString;
            this.FirstDateString = firstDateString;
            this.LastDateString = lastdateString;
            this.Comments = string.IsNullOrWhiteSpace(comments) ? null : comments.Trim();

            this.Books = this.BooksString.
                Split(',').
                Select(b => b.Trim()).
                Distinct().
                Select(b => new Book()
                {
                    Name = b.Truncate(255),
                    NormalizedName = b.Truncate(255).ToUpperInvariant(),
                    CreatedAt = DateTimeOffset.UtcNow
                }).
                ToList();
        }

        public Expression<Func<Course, bool>> GetUniqueExpression(SchoolUnique schoolUnique) => c =>
            c.School.NormalizedName == schoolUnique.NormalizedSchoolName &&
            c.School.NormalizedCity == schoolUnique.NormalizedSchoolCity &&
            c.Code == this.Code;

        public Expression<Func<Course, bool>> GetUniqueExpression(int schoolId) => c =>
            c.School.Id == schoolId &&
            c.Code == this.Code;

        public CourseUnique GetCourseUnique(SchoolUnique schoolUnique) => new(schoolUnique, this.Code);
        public DateTimeOffset GetFirstDate(string schoolTimeZone) => CalendarExtensions.ParseExact(this.FirstDateString, "d/M/yyyy", schoolTimeZone);
        public DateTimeOffset GetLastDate(string schoolTimeZone)  => CalendarExtensions.ParseExact(this.LastDateString, "d/M/yyyy", schoolTimeZone);
    }
}
