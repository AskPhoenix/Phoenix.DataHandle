using System;
using System.Collections.Generic;
using Phoenix.DataHandle.Main.Relationships;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IAspNetUsers
    {
        string UserName { get; set; }
        string NormalizedUserName { get; set; }
        string Email { get; set; }
        string NormalizedEmail { get; set; }
        bool EmailConfirmed { get; set; }
        string PasswordHash { get; set; }
        string SecurityStamp { get; set; }
        string ConcurrencyStamp { get; set; }
        string PhoneNumber { get; set; }
        bool PhoneNumberConfirmed { get; set; }
        bool TwoFactorEnabled { get; set; }
        DateTimeOffset? LockoutEnd { get; set; }
        bool LockoutEnabled { get; set; }
        int AccessFailedCount { get; set; }
        DateTimeOffset RegisteredAt { get; }
        ApplicationType CreatedApplicationType { get; set; }

        IUser User { get; }
        IEnumerable<IAspNetUserLogins> AspNetUserLogins { get; }
        IEnumerable<IAspNetUserRoles> Roles { get; }
        IEnumerable<IAttendance> Attendances { get; }
        IEnumerable<IStudentCourse> StudentCourses { get; }
        IEnumerable<IStudentExam> StudentExams { get; }
        IEnumerable<IStudentExercise> StudentExercises { get; }
        IEnumerable<ITeacherCourse> TeacherCourses { get; }
        IEnumerable<IUserSchool> UserSchools { get; }
    }
}
