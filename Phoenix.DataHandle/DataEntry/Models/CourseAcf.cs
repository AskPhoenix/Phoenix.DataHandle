﻿using Newtonsoft.Json;
using Phoenix.DataHandle.DataEntry.Models.Extensions;
using Phoenix.DataHandle.DataEntry.Models.Uniques;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Utilities;

namespace Phoenix.DataHandle.DataEntry.Models
{
    public class CourseAcf : IModelAcf, ICourse
    {
        private const string DateFormat = "d/M/yyyy";

        private CourseAcf()
        {
            this.Books = new List<IBook>();

            this.Grades = new List<IGrade>();
            this.Lectures = new List<ILecture>();
            this.Schedules = new List<ISchedule>();
            this.Users = new List<IUser>();
            this.Broadcasts = new List<IBroadcast>();
        }

        [JsonConstructor]
        public CourseAcf(short code, string name, string? subcourse, string level, string group,
            string? comments, string first_date, string last_date, string books)
            : this()
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(level))
                throw new ArgumentNullException(nameof(level));
            if (string.IsNullOrWhiteSpace(group))
                throw new ArgumentNullException(nameof(group));
            if (string.IsNullOrWhiteSpace(first_date))
                throw new ArgumentNullException(nameof(first_date));
            if (string.IsNullOrWhiteSpace(last_date))
                throw new ArgumentNullException(nameof(last_date));
            if (string.IsNullOrWhiteSpace(books))
                throw new ArgumentNullException(nameof(books));

            this.Code = code;
            this.Name = name.Trim().ToTitleCase();
            this.SubCourse = string.IsNullOrWhiteSpace(subcourse) ? null : subcourse.Trim().ToTitleCase();
            this.Level = level.Trim().ToTitleCase();
            this.Group = group.Trim();
            this.Comments = string.IsNullOrWhiteSpace(comments) ? null : comments.Trim();

            this.FirstDate = CalendarExtensions.ParseExact(first_date, DateFormat);
            this.LastDate = CalendarExtensions.ParseExact(last_date, DateFormat);

            this.Books = books
                .Split(',')
                .Select(s => s.Trim())
                .Distinct()
                .Select(b => (IBook) new Book() { Name = b })
                .ToList();

            this.BooksString = books;
            this.FirstDateString = first_date;
            this.LastDateString = last_date;
        }

        public Course ToCourse(int schoolId)
        {
            return new()
            {
                SchoolId = schoolId,
                Code = this.Code,
                Name = this.Name,
                SubCourse = this.SubCourse,
                Level = this.Level,
                Group = this.Group,
                Comments = this.Comments,
                FirstDate = this.FirstDate,
                LastDate = this.LastDate
            };
        }

        public Course ToCourse(Course courseFrom)
        {
            if (courseFrom is null)
                throw new ArgumentNullException(nameof(courseFrom));

            courseFrom.Code = this.Code;
            courseFrom.Name = this.Name;
            courseFrom.SubCourse = this.SubCourse;
            courseFrom.Level = this.Level;
            courseFrom.Group = this.Group;
            courseFrom.Comments = this.Comments;
            courseFrom.FirstDate = this.FirstDate;
            courseFrom.LastDate = this.LastDate;

            return courseFrom;
        }

        public HashSet<Book> GetBooks()
        {
            var books = this.Books.Cast<Book>().ToHashSet();
            foreach (var book in books)
                book.Normalize();
            
            return books;
        }

        public CourseUnique GetCourseUnique(SchoolUnique schoolUnique) => new(schoolUnique, this.Code);

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

        [JsonProperty(PropertyName = "books")]
        public string BooksString { get; } = null!;

        [JsonProperty(PropertyName = "first_date")]
        public string FirstDateString { get; } = null!;

        [JsonProperty(PropertyName = "last_date")]
        public string LastDateString { get; } = null!;

        [JsonProperty(PropertyName = "comments")]
        public string? Comments { get; }

        [JsonIgnore]
        public DateTime FirstDate { get; private set; }

        [JsonIgnore]
        public DateTime LastDate { get; private set; }

        [JsonIgnore]
        public List<IBook> Books { get; }


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
        public IEnumerable<IUser> Users { get; set; }

        [JsonIgnore]
        public IEnumerable<IBroadcast> Broadcasts { get; }
    }
}
