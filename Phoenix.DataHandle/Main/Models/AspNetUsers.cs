using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class AspNetUsers
    {
        public AspNetUsers()
        {
            AspNetUserLogins = new HashSet<AspNetUserLogins>();
            AspNetUserRoles = new HashSet<AspNetUserRoles>();
            Attendance = new HashSet<Attendance>();
            BotFeedback = new HashSet<BotFeedback>();
            StudentCourse = new HashSet<StudentCourse>();
            StudentExam = new HashSet<StudentExam>();
            StudentExercise = new HashSet<StudentExercise>();
            TeacherCourse = new HashSet<TeacherCourse>();
            UserSchool = new HashSet<UserSchool>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public ApplicationType CreatedApplicationType { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual ICollection<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual ICollection<Attendance> Attendance { get; set; }
        public virtual ICollection<BotFeedback> BotFeedback { get; set; }
        public virtual ICollection<StudentCourse> StudentCourse { get; set; }
        public virtual ICollection<StudentExam> StudentExam { get; set; }
        public virtual ICollection<StudentExercise> StudentExercise { get; set; }
        public virtual ICollection<TeacherCourse> TeacherCourse { get; set; }
        public virtual ICollection<UserSchool> UserSchool { get; set; }
    }
}
