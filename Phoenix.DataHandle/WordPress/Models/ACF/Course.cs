using Newtonsoft.Json;
using Phoenix.DataHandle.Utilities;
using System;
using System.Linq;

namespace Phoenix.DataHandle.WordPress.ACF
{
    class Course
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

        public bool MatchesUnique(Main.Models.Course ctxCourse)
        {
            return ctxCourse != null
                && ctxCourse.SchoolId == this.SchoolId
                && ctxCourse.Code == this.Code;
        }

        public Main.Models.Course ToContextCourse(int schoolId)
        {
            return new Main.Models.Course()
            {
                SchoolId = schoolId,
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

        public Main.Models.Book[] ExtractBooks()
        {
            if (string.IsNullOrEmpty(this.BooksString))
                return new Main.Models.Book[0];

            return this.BooksString.
                Split(',').
                Select(b => b.Trim()).
                Select(b => new Main.Models.Book()
                {
                    Name = b.Substring(0, Math.Min(b.Length, 255)),
                    CreatedAt = DateTimeOffset.Now
                }).
                ToArray();
        }

        public Course WithTitleCaseText()
        {
            return new Course()
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
    }
}
