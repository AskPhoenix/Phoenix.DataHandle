namespace Phoenix.DataHandle.Main.Models
{
    public partial class Course
    {
        public Course()
        {
            Grades = new HashSet<Grade>();
            Lectures = new HashSet<Lecture>();
            Schedules = new HashSet<Schedule>();
            Books = new HashSet<Book>();
            Broadcasts = new HashSet<Broadcast>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public short Code { get; set; }
        public int SchoolId { get; set; }
        public string Name { get; set; } = null!;
        public string? SubCourse { get; set; }
        public string Level { get; set; } = null!;
        public string Group { get; set; } = null!;
        public string? Comments { get; set; }
        public DateTime FirstDate { get; set; }
        public DateTime LastDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? ObviatedAt { get; set; }

        public virtual School School { get; set; } = null!;
        public virtual ICollection<Grade> Grades { get; set; }
        public virtual ICollection<Lecture> Lectures { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }

        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<Broadcast> Broadcasts { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
