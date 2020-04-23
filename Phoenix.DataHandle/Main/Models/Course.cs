using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class Course
    {
        public Course()
        {
            CourseBook = new HashSet<CourseBook>();
            Exam = new HashSet<Exam>();
            Lecture = new HashSet<Lecture>();
            StudentCourse = new HashSet<StudentCourse>();
            TeacherCourse = new HashSet<TeacherCourse>();
        }

        public int Id { get; set; }
        public int SchoolId { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }
        public string Group { get; set; }
        public string Info { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual School School { get; set; }
        public virtual ICollection<CourseBook> CourseBook { get; set; }
        public virtual ICollection<Exam> Exam { get; set; }
        public virtual ICollection<Lecture> Lecture { get; set; }
        public virtual ICollection<StudentCourse> StudentCourse { get; set; }
        public virtual ICollection<TeacherCourse> TeacherCourse { get; set; }
    }
}
