using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models.Extensions;
using Phoenix.DataHandle.Main.Relationships;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class AspNetRoles : IAspNetRoles, IModelEntity
    {
        IEnumerable<IAspNetUserRoles> IAspNetRoles.AspNetUserRoles => this.AspNetUserRoles;
    }

    public partial class AspNetUserLogins : IAspNetUserLogins
    {
        IAspNetUsers IAspNetUserLogins.User => this.User;
    }

    public partial class AspNetUserRoles : IAspNetUserRoles
    {
        IAspNetUsers IAspNetUserRoles.User => this.User;
        IAspNetRoles IAspNetUserRoles.Role => this.Role;
    }

    public partial class AspNetUsers : IAspNetUsers, IModelEntity
    {
        public DateTimeOffset RegisteredAt => this.CreatedAt;

        IUser IAspNetUsers.User => this.User;

        IEnumerable<IAspNetUserLogins> IAspNetUsers.AspNetUserLogins => this.AspNetUserLogins;
        IEnumerable<IAspNetUserRoles> IAspNetUsers.Roles => this.AspNetUserRoles;
        IEnumerable<IAttendance> IAspNetUsers.Attendances => this.Attendance;
        IEnumerable<IParenthood> IAspNetUsers.Children => this.ParenthoodChild;
        IEnumerable<IParenthood> IAspNetUsers.Parents => this.ParenthoodParent;
        IEnumerable<IStudentCourse> IAspNetUsers.StudentCourses => this.StudentCourse;
        IEnumerable<IStudentExam> IAspNetUsers.StudentExams => this.StudentExam;
        IEnumerable<IStudentExercise> IAspNetUsers.StudentExercises => this.StudentExercise;
        IEnumerable<ITeacherCourse> IAspNetUsers.TeacherCourses => this.TeacherCourse;
        IEnumerable<IUserSchool> IAspNetUsers.UserSchools => this.UserSchool;

        public string GetHashSignature()
        {
            byte[] salt = new byte[16];
            using var pbkdf2 = new Rfc2898DeriveBytes(this.Id + this.PhoneNumber, salt, 10 * 1000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(32);
            string savedPasswordHash = Convert.ToBase64String(hash);

            return savedPasswordHash;
        }

        public bool VerifyHashSignature(string hashSignature)
        {
            return hashSignature == this.GetHashSignature();
        }
    }

    public partial class Attendance : IAttendance
    {
        IAspNetUsers IAttendance.Student => this.Student;
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
        IAspNetUsers IBotFeedback.Author => this.Author;
    }

    public partial class Classroom : IClassroom, IModelEntity
    {
        ISchool IClassroom.School => this.School;

        IEnumerable<ILecture> IClassroom.Lectures => this.Lecture;
        IEnumerable<ISchedule> IClassroom.Schedules => this.Schedule;
    }

    public partial class Course : ICourse, IModelEntity
    {
        ISchool ICourse.School => this.School;

        IEnumerable<ICourseBook> ICourse.CourseBooks => this.CourseBook;
        IEnumerable<ILecture> ICourse.Lectures => this.Lecture;
        IEnumerable<ISchedule> ICourse.Schedules => this.Schedule;
        IEnumerable<IStudentCourse> ICourse.StudentCourses => this.StudentCourse;
        IEnumerable<ITeacherCourse> ICourse.TeacherCourses => this.TeacherCourse;

        public string NameWithSubcourse { 
            get
            {
                string tore = this.Name;
                if (!string.IsNullOrEmpty(this.SubCourse))
                    tore += " - " + this.SubCourse;

                return tore;
            }
        }
    }

    public partial class CourseBook : ICourseBook
    {
        ICourse ICourseBook.Course => this.Course;
        IBook ICourseBook.Book => this.Book;
    }

    public partial class Exam : IExam, IModelEntity
    {
        ILecture IExam.Lecture => this.Lecture;

        IEnumerable<IMaterial> IExam.Materials => this.Material;
        IEnumerable<IStudentExam> IExam.StudentExams => this.StudentExam;
    }

    public partial class Exercise : IExercise, IModelEntity
    {
        IBook IExercise.Book => this.Book;

        ILecture IExercise.Lecture => this.Lecture;
        IEnumerable<IStudentExercise> IExercise.StudentExercises => this.StudentExercise;
    }

    public partial class Lecture : ILecture, IModelEntity
    {
        ICourse ILecture.Course => this.Course;
        ISchedule ILecture.Schedule => this.Schedule;
        IClassroom ILecture.Classroom => this.Classroom;
        IExam ILecture.Exam => this.Exam;

        IEnumerable<IAttendance> ILecture.Attendances => this.Attendance;
        IEnumerable<IExercise> ILecture.Exercises => this.Exercise;
    }

    public partial class Material : IMaterial, IModelEntity
    {
        IExam IMaterial.Exam => this.Exam;
        IBook IMaterial.Book => this.Book;
    }

    public partial class School : ISchool, IModelEntity
    {
        IEnumerable<IUserSchool> ISchool.UserSchools => this.UserSchool;
        IEnumerable<IClassroom> ISchool.Classrooms => this.Classroom;
        IEnumerable<ICourse> ISchool.Courses => this.Course;
    }

    public partial class StudentCourse : IStudentCourse
    {
        IAspNetUsers IStudentCourse.Student => this.Student;
        ICourse IStudentCourse.Course => this.Course;
    }

    public partial class StudentExam : IStudentExam
    {
        IAspNetUsers IStudentExam.Student => this.Student;
        IExam IStudentExam.Exam => this.Exam;
    }

    public partial class StudentExercise : IStudentExercise
    {
        IAspNetUsers IStudentExercise.Student => this.Student;
        IExercise IStudentExercise.Exercise => this.Exercise;
    }

    public partial class TeacherCourse : ITeacherCourse
    {
        IAspNetUsers ITeacherCourse.Teacher => this.Teacher;
        ICourse ITeacherCourse.Course => this.Course;
    }

    public partial class User : IUser
    {
        public string FullName => (this.LastName + " " + this.FirstName).Trim();
        public int Id => this.AspNetUser.Id;
        IAspNetUsers IUser.AspNetUser => this.AspNetUser;
    }

    public partial class Parenthood : IParenthood
    {
        IAspNetUsers IParenthood.Child => this.Child;
        IAspNetUsers IParenthood.Parent => this.Parent;
    }

    public partial class Schedule : ISchedule, IModelEntity
    {
        ICourse ISchedule.Course => this.Course;
        IClassroom ISchedule.Classroom => this.Classroom;

        IEnumerable<ILecture> ISchedule.Lectures => this.Lecture;
    }

    public partial class UserSchool : IUserSchool
    {
        IAspNetUsers IUserSchool.AspNetUser => this.AspNetUser;

        ISchool IUserSchool.School => this.School;
    }
}
