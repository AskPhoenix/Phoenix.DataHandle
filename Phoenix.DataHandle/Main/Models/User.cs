using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class User
    {
        public User()
        {
            this.Attendance = new HashSet<Attendance>();
            this.BotFeedback = new HashSet<BotFeedback>();
            this.StudentCourse = new HashSet<StudentCourse>();
            this.StudentExam = new HashSet<StudentExam>();
            this.StudentExercise = new HashSet<StudentExercise>();
            this.TeacherCourse = new HashSet<TeacherCourse>();
        }

        public int AspNetUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual AspNetUsers AspNetUser { get; set; }
        public virtual ICollection<Attendance> Attendance { get; set; }
        public virtual ICollection<BotFeedback> BotFeedback { get; set; }
        public virtual ICollection<StudentCourse> StudentCourse { get; set; }
        public virtual ICollection<StudentExam> StudentExam { get; set; }
        public virtual ICollection<StudentExercise> StudentExercise { get; set; }
        public virtual ICollection<TeacherCourse> TeacherCourse { get; set; }
    }
}
