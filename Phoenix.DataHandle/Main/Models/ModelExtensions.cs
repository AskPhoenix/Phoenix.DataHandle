using System;
using System.Collections.Generic;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models.Extensions;
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

        IEnumerable<IAspNetUserRoles> IAspNetUsers.Roles => this.AspNetUserRoles;
    }

    public partial class Attendance : IAttendance
    {
        IUser IAttendance.Student => this.Student;
        ILecture IAttendance.Lecture => this.Lecture;
    }

    public partial class Book : IBook, IModelEntity
    {
        IEnumerable<ICourseBook> IBook.CourseBooks => this.CourseBook;
        IEnumerable<IExercise> IBook.Exercises => this.Exercise;
        IEnumerable<IMaterial> IBook.Materials => this.Material;
    }

    public partial class BotFeedback : IBotFeedback, IModelEntity
    {
        IUser IBotFeedback.Author => this.Author;
    }

    public partial class Classroom : IClassroom, IModelEntity
    {
        ISchool IClassroom.School => this.School;

        IEnumerable<IExam> IClassroom.Exams => this.Exam;
        IEnumerable<ILecture> IClassroom.Lectures => this.Lecture;
    }

    public partial class Course : ICourse, IModelEntity
    {
        ISchool ICourse.School => this.School;

        IEnumerable<ICourseBook> ICourse.CourseBooks => this.CourseBook;
        IEnumerable<IExam> ICourse.Exams => this.Exam;
        IEnumerable<ILecture> ICourse.Lectures => this.Lecture;
        IEnumerable<IStudentCourse> ICourse.StudentCourses => this.StudentCourse;
        IEnumerable<ITeacherCourse> ICourse.TeacherCourses => this.TeacherCourse;
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

        IEnumerable<IMaterial> IExam.Materials => this.Material;
        IEnumerable<IStudentExam> IExam.StudentExams => this.StudentExam;
    }

    public partial class Exercise : IExercise, IModelEntity
    {
        IBook IExercise.Book => this.Book;

        IEnumerable<IHomework> IExercise.Homeworks => this.Homework;
        IEnumerable<IStudentExercise> IExercise.StudentExercises => this.StudentExercise;
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

        IEnumerable<IAttendance> ILecture.Attendances => this.Attendance;
        IEnumerable<IHomework> ILecture.Homeworks => this.Homework;
    }

    public partial class Material : IMaterial, IModelEntity
    {
        IExam IMaterial.Exam => this.Exam;
        IBook IMaterial.Book => this.Book;
    }

    public partial class School : ISchool, IModelEntity
    {
        IEnumerable<IClassroom> ISchool.Classrooms => this.Classroom;
        IEnumerable<ICourse> ISchool.Courses => this.Course;
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
        int IModelEntity.Id => AspNetUserId;
        IAspNetUsers IUser.AspNetUser => this.AspNetUser;

        IEnumerable<IAttendance> IUser.Attendances => this.Attendance;
        IEnumerable<IStudentCourse> IUser.StudentCourses => this.StudentCourse;
        IEnumerable<IStudentExam> IUser.StudentExams => this.StudentExam;
        IEnumerable<IStudentExercise> IUser.StudentExercises => this.StudentExercise;
        IEnumerable<ITeacherCourse> IUser.TeacherCourses => this.TeacherCourse;
    }

    // TODO: to be created in database
    public partial class Schedule : ISchedule, IModelEntity
    {
        public int Id { get; }
        public DayOfWeek dayOfWeek { get; set; }
        public DateTime startAt { get; set; }
        public DateTime endAt { get; set; }
        public ICourse Course { get; }
    }

}
