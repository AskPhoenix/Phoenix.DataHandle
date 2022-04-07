using Newtonsoft.Json;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Utilities;
using Phoenix.DataHandle.DataEntry.Models.Uniques;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Phoenix.DataHandle.DataEntry.Models.Extensions;
using Phoenix.DataHandle.Main.Entities;

namespace Phoenix.DataHandle.DataEntry.Models
{
    public class CourseAcf : IModelAcf, ICourse
    {
        private CourseAcf()
        {
            this.Books = new List<IBook>();

            this.Grades = new List<IGrade>();
            this.Lectures = new List<ILecture>();
            this.Schedules = new List<ISchedule>();
            this.Users = new List<IAspNetUser>();
            this.Broadcasts = new List<IBroadcast>();
        }

        [JsonConstructor]
        public CourseAcf(short code, string name, string? subcourse, string level, string group, string? comments,
            string firstDateString, string lastdateString, string booksString)
            : this()
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(level))
                throw new ArgumentNullException(nameof(level));
            if (string.IsNullOrWhiteSpace(group))
                throw new ArgumentNullException(nameof(group));
            if (string.IsNullOrWhiteSpace(firstDateString))
                throw new ArgumentNullException(nameof(firstDateString));
            if (string.IsNullOrWhiteSpace(lastdateString))
                throw new ArgumentNullException(nameof(lastdateString));
            if (string.IsNullOrWhiteSpace(booksString))
                throw new ArgumentNullException(nameof(booksString));

            this.Code = code;
            this.Name = name.Trim().Truncate(150).ToTitleCase();
            this.SubCourse = string.IsNullOrWhiteSpace(subcourse) ? null : subcourse.Trim().Truncate(150).ToTitleCase();
            this.Level = level.Trim().Truncate(50).ToTitleCase();
            this.Group = group.Trim().Truncate(50);
            this.Comments = string.IsNullOrWhiteSpace(comments) ? null : comments.Trim();
            
            this.FirstDate = GetCourseDate(firstDateString, selFirstDate: true);
            this.LastDate = GetCourseDate(lastdateString, selFirstDate: false);

            this.Books = booksString
                .Split(',')
                .Select(s => s.Trim())
                .Distinct()
                .Select(b => (IBook)new Book()
                {
                    Name = b.Truncate(255)
                })
                .ToList();
        }

        public Expression<Func<Course, bool>> GetUniqueExpression(SchoolUnique schoolUnique) => c =>
            c.School.Code == schoolUnique.Code &&
            c.Code == this.Code;

        public Expression<Func<Course, bool>> GetUniqueExpression(int schoolId) => c =>
            c.School.Id == schoolId &&
            c.Code == this.Code;

        public CourseUnique GetCourseUnique(SchoolUnique schoolUnique) => new(schoolUnique, this.Code);

        private DateTimeOffset GetCourseDate(string dateString, bool selFirstDate) => 
            CalendarExtensions.ParseExact(dateString, "d/M/yyyy", this.TimeZone);


        [JsonProperty(PropertyName = "code")]
        public short Code { get; }

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

        [JsonIgnore]
        public DateTimeOffset FirstDate { get; private set; }

        [JsonIgnore]
        public DateTimeOffset LastDate { get; private set; }

        [JsonIgnore]
        public List<IBook> Books { get; }

        // TODO: Check that when TimeZone is set, the First/Last Date properties have already a value
        [JsonIgnore]
        public string TimeZone
        {
            get => timezone;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException(nameof(TimeZone));

                timezone = value;

                this.FirstDate = this.FirstDate.SetOffsetFromTimeZone(timezone);
                this.LastDate = this.LastDate.SetOffsetFromTimeZone(timezone);
            }
        }
        private string timezone = "UTC";


        [JsonIgnore]
        public ISchool School { get; } = null!;

        [JsonIgnore]
        public IEnumerable<IGrade> Grades { get; }

        [JsonIgnore]
        public IEnumerable<ILecture> Lectures { get; set; }

        [JsonIgnore]
        public IEnumerable<ISchedule> Schedules { get; set; }

        IEnumerable<IBook> ICourse.Books => this.Books;

        [JsonIgnore]
        public IEnumerable<IAspNetUser> Users { get; set; }

        [JsonIgnore]
        public IEnumerable<IBroadcast> Broadcasts { get; }
    }
}
