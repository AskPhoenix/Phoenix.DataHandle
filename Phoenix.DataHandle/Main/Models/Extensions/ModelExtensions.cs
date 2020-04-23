using System.Collections.Generic;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main;
using Phoenix.DataHandle.Main.Relationships;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class AspNetRoles : IAspNetRoles, IModelEntity
    {
        Role IAspNetRoles.Type { get => (Role)this.Type; set => this.Type = (int)value; }
        IEnumerable<IAspNetUserRoles> IAspNetRoles.AspNetUserRoles => this.AspNetUserRoles;
    }

    public partial class AspNetUserRoles : IAspNetUserRoles
    {
        IAspNetUsers IAspNetUserRoles.User => this.User;
        IAspNetRoles IAspNetUserRoles.Role => this.Role;
    }

    public partial class AspNetUsers : IAspNetUsers, IModelEntity
    {
        IUser IAspNetUsers.User => this.User;

        public IEnumerable<IAspNetUserRoles> Roles => this.AspNetUserRoles;
    }

    public partial class Attendance : IAttendance
    {
        IUser IAttendance.Student => this.Student;
        ILecture IAttendance.Lecture => this.Lecture;
    }

    public partial class Book : IBook, IModelEntity
    {
        public IEnumerable<ICourseBook> CourseBooks => this.CourseBook;
        public IEnumerable<IExercise> Exercises => this.Exercise;
        public IEnumerable<IMaterial> Materials => this.Material;
    }

    public partial class Classroom : IClassroom, IModelEntity
    {
        ISchool IClassroom.School => this.School;

        public IEnumerable<IExam> Exams => this.Exam;
        public IEnumerable<ILecture> Lectures => this.Lecture;
    }

    public partial class Course : ICourse, IModelEntity
    {
        ISchool ICourse.School => this.School;

        public IEnumerable<ICourseBook> CourseBooks => this.CourseBook;
        public IEnumerable<IExam> Exams => this.Exam;
        public IEnumerable<ILecture> Lectures => this.Lecture;
        public IEnumerable<IStudentCourse> StudentCourses => this.StudentCourse;
        public IEnumerable<ITeacherCourse> TeacherCourses => this.TeacherCourse;
    }

    public partial class CourseBook : ICourseBook
    {
        ICourse ICourseBook.Course => this.Course;
        IBook ICourseBook.Book => this.Book;
    }

    public partial class Exam : IExam, IModelEntity
    {
        ICourse IExam.Course => this.Course;
        IClassroom IExam.Classroom => this.Classroom;

        public IEnumerable<IMaterial> Materials => this.Material;
        public IEnumerable<IStudentExam> StudentExams => this.StudentExam;
    }

    public partial class Exercise : IExercise, IModelEntity
    {
        IBook IExercise.Book => this.Book;

        public IEnumerable<IHomework> Homeworks => this.Homework;
        public IEnumerable<IStudentExercise> StudentExercises => this.StudentExercise;
    }

    public partial class Homework : IHomework
    {
        ILecture IHomework.ForLecture => this.ForLecture;
        IExercise IHomework.Exercise => this.Exercise;
    }

    public partial class Lecture : ILecture, IModelEntity
    {
        ICourse ILecture.Course => this.Course;
        IClassroom ILecture.Classroom => this.Classroom;

        public IEnumerable<IAttendance> Attendances => this.Attendance;
        public IEnumerable<IHomework> Homeworks => this.Homework;
    }

    public partial class Material : IMaterial, IModelEntity
    {
        IExam IMaterial.Exam => this.Exam;
        IBook IMaterial.Book => this.Book;
    }

    public partial class School : ISchool, IModelEntity
    {
        public IEnumerable<IClassroom> Classrooms => this.Classroom;
        public IEnumerable<ICourse> Courses => this.Course;
    }

    public partial class StudentCourse : IStudentCourse
    {
        IUser IStudentCourse.Student => this.Student;
        ICourse IStudentCourse.Course => this.Course;
    }

    public partial class StudentExam : IStudentExam
    {
        IUser IStudentExam.Student => this.Student;
        IExam IStudentExam.Exam => this.Exam;
    }

    public partial class StudentExercise : IStudentExercise
    {
        IUser IStudentExercise.Student => this.Student;
        IExercise IStudentExercise.Exercise => this.Exercise;
    }

    public partial class TeacherCourse : ITeacherCourse
    {
        IUser ITeacherCourse.Teacher => this.Teacher;
        ICourse ITeacherCourse.Course => this.Course;
    }

    public partial class User : IUser, IModelEntity
    {
        public int Id => AspNetUserId;
        IAspNetUsers IUser.AspNetUser => this.AspNetUser;

        public IEnumerable<IAttendance> Attendances => this.Attendance;
        public IEnumerable<IStudentCourse> StudentCourses => this.StudentCourse;
        public IEnumerable<IStudentExam> StudentExams => this.StudentExam;
        public IEnumerable<IStudentExercise> StudentExercises => this.StudentExercise;
        public IEnumerable<ITeacherCourse> TeacherCourses => this.TeacherCourse;
    }
}
