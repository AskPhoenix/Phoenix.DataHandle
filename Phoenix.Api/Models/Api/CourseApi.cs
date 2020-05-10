using System;
using System.Collections.Generic;
using System.Linq;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Relationships;

namespace Phoenix.Api.Models.Api
{
    public class CourseApi : ICourse, IModelApi
    {
        public int id { get; set; }
        public ISchool School { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }
        public string Group { get; set; }
        public string Info { get; set; }
        public IEnumerable<ICourseBook> CourseBooks { get; }
        public IEnumerable<IExam> Exams { get; }
        public IEnumerable<ILecture> Lectures { get; }
        public IEnumerable<IStudentCourse> StudentCourses { get; }
        public IEnumerable<ITeacherCourse> TeacherCourses { get; }
    }
}
