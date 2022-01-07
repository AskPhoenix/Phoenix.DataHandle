using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Phoenix.DataHandle.Main.Models
{
    public partial class Course
    {
        public Course()
        {
            Broadcast = new HashSet<Broadcast>();
            CourseBook = new HashSet<CourseBook>();
            Lecture = new HashSet<Lecture>();
            Schedule = new HashSet<Schedule>();
            StudentCourse = new HashSet<StudentCourse>();
            TeacherCourse = new HashSet<TeacherCourse>();
        }

        public int Id { get; set; }
        public short Code { get; set; }
        public int SchoolId { get; set; }
        public string Name { get; set; }
        public string SubCourse { get; set; }
        public string Level { get; set; }
        public string Group { get; set; }
        public string Info { get; set; }
        public DateTimeOffset FirstDate { get; set; }
        public DateTimeOffset LastDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }

        public virtual School School { get; set; }
        public virtual ICollection<Broadcast> Broadcast { get; set; }
        public virtual ICollection<CourseBook> CourseBook { get; set; }
        public virtual ICollection<Lecture> Lecture { get; set; }
        public virtual ICollection<Schedule> Schedule { get; set; }
        public virtual ICollection<StudentCourse> StudentCourse { get; set; }
        public virtual ICollection<TeacherCourse> TeacherCourse { get; set; }
    }
}
