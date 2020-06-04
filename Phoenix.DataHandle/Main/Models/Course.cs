using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class Course
    {
        public Course()
        {
            this.CourseBook = new HashSet<CourseBook>();
            this.Lecture = new HashSet<Lecture>();
            this.Schedule = new HashSet<Schedule>();
            this.StudentCourse = new HashSet<StudentCourse>();
            this.TeacherCourse = new HashSet<TeacherCourse>();
        }

        public int Id { get; set; }
        public int SchoolId { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }
        public string Group { get; set; }
        public string Info { get; set; }
        public DateTime FirstDate { get; set; }
        public DateTime LastDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual School School { get; set; }
        public virtual ICollection<CourseBook> CourseBook { get; set; }
        public virtual ICollection<Lecture> Lecture { get; set; }
        public virtual ICollection<Schedule> Schedule { get; set; }
        public virtual ICollection<StudentCourse> StudentCourse { get; set; }
        public virtual ICollection<TeacherCourse> TeacherCourse { get; set; }
    }
}
