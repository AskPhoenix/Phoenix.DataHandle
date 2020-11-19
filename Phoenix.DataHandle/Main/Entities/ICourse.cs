using System;
using System.Collections.Generic;
using Phoenix.DataHandle.Main.Relationships;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface ICourse
    {
        ISchool School { get; }
        string Name { get; set; }
        string SubCourse { get; set; }
        string Level { get; set; }
        string Group { get; set; }
        string Info { get; set; }
        DateTimeOffset FirstDate { get; set; }
        DateTimeOffset LastDate { get; set; }
        DateTimeOffset CreatedAt { get; set; }
        DateTimeOffset? UpdatedAt { get; set; }

        IEnumerable<ICourseBook> CourseBooks { get; }
        IEnumerable<ILecture> Lectures { get; }
        IEnumerable<ISchedule> Schedules { get; }
        IEnumerable<IStudentCourse> StudentCourses { get; }
        IEnumerable<ITeacherCourse> TeacherCourses { get; }
    }
}