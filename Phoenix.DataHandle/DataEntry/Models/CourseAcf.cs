using Newtonsoft.Json;
using Phoenix.DataHandle.Base.Entities;
using Phoenix.DataHandle.DataEntry.Entities;
using Phoenix.DataHandle.DataEntry.Models.Extensions;
using Phoenix.DataHandle.DataEntry.Types.Uniques;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Utilities;

namespace Phoenix.DataHandle.DataEntry.Models
{
    public class CourseAcf : IModelAcf, ICourseAcf
    {
        private const string DateFormat = "d/M/yyyy";

        private CourseAcf()
        {
            this.Books = new HashSet<Book>();
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
                .Select(b => new Book() { Name = b }.Normalize())
                .ToHashSet();

            this.BooksString = books;
            this.FirstDateString = first_date;
            this.LastDateString = last_date;
        }

        public Course ToCourse(int schoolId, IEnumerable<Book> booksFinal)
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
                LastDate = this.LastDate,

                Books = booksFinal.ToHashSet()
            };
        }

        public Course ToCourse(Course courseToUpdate, int schoolId, IEnumerable<Book> booksFinal)
        {
            if (courseToUpdate is null)
                throw new ArgumentNullException(nameof(courseToUpdate));

            courseToUpdate.SchoolId = schoolId;
            courseToUpdate.Code = this.Code;
            courseToUpdate.Name = this.Name;
            courseToUpdate.SubCourse = this.SubCourse;
            courseToUpdate.Level = this.Level;
            courseToUpdate.Group = this.Group;
            courseToUpdate.Comments = this.Comments;
            courseToUpdate.FirstDate = this.FirstDate;
            courseToUpdate.LastDate = this.LastDate;

            if (booksFinal is not null)
            {
                foreach (var bookFinal in booksFinal)
                    if (!courseToUpdate.Books.Contains(bookFinal))
                        courseToUpdate.Books.Add(bookFinal);

                foreach (var bookInitial in courseToUpdate.Books)
                    if (!booksFinal.Contains(bookInitial))
                        courseToUpdate.Books.Remove(bookInitial);
            }

            return courseToUpdate;
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

        [JsonProperty(PropertyName = "comments")]
        public string? Comments { get; }

        [JsonProperty(PropertyName = "first_date")]
        public string FirstDateString { get; } = null!;

        [JsonProperty(PropertyName = "last_date")]
        public string LastDateString { get; } = null!;


        [JsonIgnore]
        public DateTime FirstDate { get; private set; }

        [JsonIgnore]
        public DateTime LastDate { get; private set; }


        [JsonProperty(PropertyName = "books")]
        public string BooksString { get; } = null!;

        [JsonIgnore]
        public HashSet<Book> Books { get; }

        IEnumerable<IBookBase> ICourseAcf.Books => this.Books;
    }
}
