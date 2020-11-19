using System.Collections.Generic;
using Phoenix.DataHandle.Main.Relationships;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IUser
    {
        IAspNetUsers AspNetUser { get; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string FullName { get; }
        bool TermsAccepted { get; set; }

        IEnumerable<IAttendance> Attendances { get; }
        IEnumerable<IStudentCourse> StudentCourses { get; }
        IEnumerable<IStudentExam> StudentExams { get; }
        IEnumerable<IStudentExercise> StudentExercises { get; }
        IEnumerable<ITeacherCourse> TeacherCourses { get; }
    }
}