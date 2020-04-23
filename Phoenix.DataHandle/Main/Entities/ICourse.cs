using System.Collections.Generic;
using Phoenix.DataHandle.Main.Relationships;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface ICourse
    {
        ISchool School { get; }
        string Name { get; set; }
        string Level { get; set; }
        string Group { get; set; }
        string Info { get; set; }

        IEnumerable<ICourseBook> CourseBooks { get; }
        IEnumerable<IExam> Exams { get; }
        IEnumerable<ILecture> Lectures { get; }
        IEnumerable<IStudentCourse> StudentCourses { get; }
        IEnumerable<ITeacherCourse> TeacherCourses { get; }
    }
}