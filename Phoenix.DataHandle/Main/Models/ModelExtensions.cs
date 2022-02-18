using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models.Extensions;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class AspNetRole : IAspNetRole, IModelEntity
    {
        IEnumerable<IAspNetUser> IAspNetRole.Users => this.Users;
    }

    public partial class AspNetUser : IAspNetUser, IObviableModelEntity
    {
        IUser IAspNetUser.User => this.User;

        IEnumerable<IAspNetUserLogin> IAspNetUser.AspNetUserLogins => this.AspNetUserLogins;

        IEnumerable<IBotFeedback> IAspNetUser.BotFeedbacks => this.BotFeedbacks;

        IEnumerable<IBroadcast> IAspNetUser.Broadcasts => this.Broadcasts;

        IEnumerable<IAspNetUser> IAspNetUser.Children => this.Children;

        IEnumerable<ICourse> IAspNetUser.Courses => this.Courses;
        
        IEnumerable<ILecture> IAspNetUser.Lectures => this.Lectures;

        IEnumerable<IAspNetUser> IAspNetUser.Parents => this.Parents;

        IEnumerable<IAspNetRole> IAspNetUser.Roles => this.Roles;

        IEnumerable<ISchool> IAspNetUser.Schools => this.Schools;

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

    public partial class AspNetUserLogin : IAspNetUserLogin
    {
        IAspNetUser IAspNetUserLogin.User => this.User;

        IChannel IAspNetUserLogin.Channel => this.Channel;
    }

    public partial class Book : IBook, IModelEntity
    {
        IEnumerable<IExercise> IBook.Exercises => this.Exercises;

        IEnumerable<IMaterial> IBook.Materials => this.Materials;
        
        IEnumerable<ICourse> IBook.Courses => this.Courses;
    }

    public partial class BotFeedback : IBotFeedback, IModelEntity
    {
        IAspNetUser IBotFeedback.Author => this.Author;
    }

    public partial class Broadcast : IBroadcast, IModelEntity
    {
        ISchool IBroadcast.School => this.School;

        IAspNetUser IBroadcast.Author => this.Author;

        IEnumerable<ICourse> IBroadcast.Courses => this.Courses;
    }

    public partial class Channel : IChannel, IModelEntity
    {
        IEnumerable<IAspNetUserLogin> IChannel.AspNetUserLogins => this.AspNetUserLogins;

        IEnumerable<ISchoolLogin> IChannel.SchoolLogins => this.SchoolLogins;
    }

    public partial class Classroom : IClassroom, IObviableModelEntity
    {
        ISchool IClassroom.School => this.School;

        IEnumerable<ILecture> IClassroom.Lectures => this.Lectures;

        IEnumerable<ISchedule> IClassroom.Schedules => this.Schedules;
    }

    public partial class Course : ICourse, IObviableModelEntity
    {
        ISchool ICourse.School => this.School;

        IEnumerable<ILecture> ICourse.Lectures => this.Lectures;

        IEnumerable<ISchedule> ICourse.Schedules => this.Schedules;

        IEnumerable<IBook> ICourse.Books => this.Books;

        IEnumerable<IBroadcast> ICourse.Broadcasts => this.Broadcasts;

        IEnumerable<IAspNetUser> ICourse.Teachers => this.Teachers;

        public string NameWithSubcourse
        {
            get
            {
                string tore = this.Name;
                if (!string.IsNullOrEmpty(this.SubCourse))
                    tore += " - " + this.SubCourse;

                return tore;
            }
        }
    }

    public partial class Exam : IExam, IModelEntity
    {
        ILecture IExam.Lecture => this.Lecture;

        IEnumerable<IMaterial> IExam.Materials => this.Materials;
    }

    public partial class Exercise : IExercise, IModelEntity
    {
        ILecture IExercise.Lecture => this.Lecture;

        IBook? IExercise.Book => this.Book;
    }

    public partial class Grade : IGrade, IModelEntity
    {
        IAspNetUser IGrade.Student => this.Student;

        ICourse? IGrade.Course => this.Course;

        IExam? IGrade.Exam => this.Exam;

        IExercise? IGrade.Exercise => this.Exercise;
    }

    public partial class Lecture : ILecture, IModelEntity
    {
        ICourse ILecture.Course => this.Course;

        IClassroom? ILecture.Classroom => this.Classroom;

        ISchedule? ILecture.Schedule => this.Schedule;

        IEnumerable<IExam> ILecture.Exams => this.Exams;

        IEnumerable<IExercise> ILecture.Exercises => this.Exercises;

        IEnumerable<IAspNetUser> ILecture.Attendees => this.Attendees;
    }

    public partial class Material : IMaterial, IModelEntity
    {
        IExam IMaterial.Exam => this.Exam;

        IBook? IMaterial.Book => this.Book;
    }

    public partial class Schedule : ISchedule, IObviableModelEntity
    {
        ICourse ISchedule.Course => this.Course;

        IClassroom? ISchedule.Classroom => this.Classroom;

        IEnumerable<ILecture> ISchedule.Lectures => this.Lectures;
    }

    public partial class School : ISchool, IObviableModelEntity
    {
        ISchoolInfo ISchool.SchoolInfo => this.SchoolInfo;
        IEnumerable<IBroadcast> ISchool.Broadcasts => this.Broadcasts;

        IEnumerable<IClassroom> ISchool.Classrooms => this.Classrooms;

        IEnumerable<ICourse> ISchool.Courses => this.Courses;

        IEnumerable<ISchoolLogin> ISchool.SchoolLogins => this.SchoolLogins;

        IEnumerable<IAspNetUser> ISchool.Users => this.Users;
    }

    public partial class SchoolInfo : ISchoolInfo, IObviableModelEntity
    {
        ISchool ISchoolInfo.School => this.School;

        public int Id => this.School.Id;

        public DateTimeOffset CreatedAt { get => this.School.CreatedAt; set => this.School.CreatedAt = value; }
        public DateTimeOffset? UpdatedAt { get => this.School.UpdatedAt; set => this.School.UpdatedAt = value; }
        public DateTimeOffset? ObviatedAt { get => this.School.ObviatedAt; set => this.School.ObviatedAt = value; }
    }

    public partial class SchoolLogin : ISchoolLogin
    {
        ISchool ISchoolLogin.School => this.School;

        IChannel ISchoolLogin.Channel => this.Channel;
    }

    public partial class User : IUser, IObviableModelEntity
    {
        public string FullName => (this.LastName + " " + this.FirstName).Trim();
        IAspNetUser IUser.AspNetUser => this.AspNetUser;

        public int Id => this.AspNetUser.Id;

        public DateTimeOffset CreatedAt { get => this.AspNetUser.CreatedAt; set => this.AspNetUser.CreatedAt = value; }
        public DateTimeOffset? UpdatedAt { get => this.AspNetUser.UpdatedAt; set => this.AspNetUser.UpdatedAt = value; }
        public DateTimeOffset? ObviatedAt { get => this.AspNetUser.ObviatedAt; set => this.AspNetUser.ObviatedAt = value; }
    }
}
