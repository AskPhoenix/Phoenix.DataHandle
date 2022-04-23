using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models.Extensions;
using Phoenix.DataHandle.Main.Types;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class Book : IBook, INormalizableEntity
    {
        IEnumerable<IExercise> IBook.Exercises => this.Exercises;
        IEnumerable<IMaterial> IBook.Materials => this.Materials;
        
        IEnumerable<ICourse> IBook.Courses => this.Courses;

        public static Func<string, string> NormFunc => s => s.ToUpperInvariant();
        public void Normalize()
        {
            this.NormalizedName = Book.NormFunc(this.NormalizedName);
        }
    }

    public partial class BotFeedback : IBotFeedback, IModelEntity
    {
        IUser IBotFeedback.Author => this.Author;
    }

    public partial class Broadcast : IBroadcast, IModelEntity
    {
        ISchool IBroadcast.School => this.School;
        IUser IBroadcast.Author => this.Author;

        IEnumerable<ICourse> IBroadcast.Courses => this.Courses;
    }

    public partial class Channel : IChannel, INormalizableEntity
    {
        IEnumerable<ISchoolLogin> IChannel.SchoolLogins => this.SchoolLogins;
        IEnumerable<IUserLogin> IChannel.UserLogins => this.UserLogins;

        public void Normalize()
        {
            this.ProviderName = this.Provider.ToString();
        }
    }

    public partial class Classroom : IClassroom, IObviableModelEntity, INormalizableEntity
    {
        ISchool IClassroom.School => this.School;

        IEnumerable<ILecture> IClassroom.Lectures => this.Lectures;
        IEnumerable<ISchedule> IClassroom.Schedules => this.Schedules;

        public static Func<string, string> NormFunc => s => s.ToUpperInvariant();
        public void Normalize()
        {
            this.NormalizedName = Classroom.NormFunc(this.NormalizedName);
        }
    }

    public partial class Course : ICourse, IObviableModelEntity
    {
        ISchool ICourse.School => this.School;

        IEnumerable<IGrade> ICourse.Grades => this.Grades;
        IEnumerable<ILecture> ICourse.Lectures => this.Lectures;
        IEnumerable<ISchedule> ICourse.Schedules => this.Schedules;

        IEnumerable<IBook> ICourse.Books => this.Books;
        IEnumerable<IBroadcast> ICourse.Broadcasts => this.Broadcasts;
        IEnumerable<IUser> ICourse.Users => this.Users;
    }

    public partial class Exam : IExam, IModelEntity
    {
        ILecture IExam.Lecture => this.Lecture;

        IEnumerable<IGrade> IExam.Grades => this.Grades;
        IEnumerable<IMaterial> IExam.Materials => this.Materials;
    }

    public partial class Exercise : IExercise, IModelEntity
    {
        ILecture IExercise.Lecture => this.Lecture;
        IBook? IExercise.Book => this.Book;

        IEnumerable<IGrade> IExercise.Grades => this.Grades;
    }

    public partial class Grade : IGrade, IModelEntity
    {
        IUser IGrade.Student => this.Student;
        ICourse? IGrade.Course => this.Course;
        IExam? IGrade.Exam => this.Exam;
        IExercise? IGrade.Exercise => this.Exercise;
    }

    public partial class Lecture : ILecture, IModelEntity
    {
        ICourse ILecture.Course => this.Course;
        IClassroom? ILecture.Classroom => this.Classroom;
        ISchedule? ILecture.Schedule => this.Schedule;
        ILecture? ILecture.ReplacementLecture => this.ReplacementLecture;

        IEnumerable<IExam> ILecture.Exams => this.Exams;
        IEnumerable<IExercise> ILecture.Exercises => this.Exercises;
        IEnumerable<ILecture> ILecture.InverseReplacementLecture => this.InverseReplacementLecture;

        IEnumerable<IUser> ILecture.Attendees => this.Attendees;
    }

    public partial class Material : IMaterial, IModelEntity
    {
        IExam IMaterial.Exam => this.Exam;
        IBook? IMaterial.Book => this.Book;
    }

    public partial class OneTimeCode : IOneTimeCode, IModelEntity
    {
        IUser IOneTimeCode.User => this.User;
    }

    public partial class Schedule : ISchedule, IObviableModelEntity
    {
        ICourse ISchedule.Course => this.Course;
        IClassroom? ISchedule.Classroom => this.Classroom;

        IEnumerable<ILecture> ISchedule.Lectures => this.Lectures;
    }

    public partial class Role : IRole, INormalizableEntity
    {
        IEnumerable<IUser> IRole.Users => this.Users;

        public void Normalize()
        {
            this.Name = this.Rank.ToString();
        }
    }

    public partial class School : ISchool, IObviableModelEntity
    {
        ISchoolInfo ISchool.SchoolInfo => this.SchoolInfo;
        IEnumerable<IBroadcast> ISchool.Broadcasts => this.Broadcasts;
        IEnumerable<IClassroom> ISchool.Classrooms => this.Classrooms;
        IEnumerable<ICourse> ISchool.Courses => this.Courses;
        IEnumerable<ISchoolLogin> ISchool.SchoolLogins => this.SchoolLogins;

        IEnumerable<IUser> ISchool.Users => this.Users;
    }

    public partial class SchoolInfo : ISchoolInfo
    {
        ISchool ISchoolInfo.School => this.School;
    }

    public partial class SchoolLogin : ISchoolLogin, ILoginEntity
    {
        ISchool ISchoolLogin.Tenant => this.Tenant;

        IChannel ILoginEntity.Channel => this.Channel;
    }

    public partial class User : IUser, IObviableModelEntity, INormalizableEntity
    {
        IUserInfo IUser.UserInfo => this.UserInfo;
        IEnumerable<IBotFeedback> IUser.BotFeedbacks => this.BotFeedbacks;
        IEnumerable<IBroadcast> IUser.Broadcasts => this.Broadcasts;
        IEnumerable<IGrade> IUser.Grades => this.Grades;
        IEnumerable<IOneTimeCode> IUser.OneTimeCodes => this.OneTimeCodes;
        IEnumerable<IUserLogin> IUser.UserLogins => this.UserLogins;

        IEnumerable<IUser> IUser.Children => this.Children;
        IEnumerable<ICourse> IUser.Courses => this.Courses;
        IEnumerable<ILecture> IUser.Lectures => this.Lectures;
        IEnumerable<IUser> IUser.Parents => this.Parents;
        IEnumerable<IRole> IUser.Roles => this.Roles;
        IEnumerable<ISchool> IUser.Schools => this.Schools;

        public static Func<string, string> NormFunc => s => s.ToUpperInvariant();

        public void Normalize()
        {
            if (this.Email is not null)
                this.NormalizedEmail = NormFunc(this.Email);
            
            this.NormalizedUserName = NormFunc(this.UserName);
        }

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

    public partial class UserInfo : IUserInfo
    {
        IUser IUserInfo.User => this.User;
        public string FullName => this.BuildFullName();
    }

    public partial class UserLogin : IUserLogin, ILoginEntity
    {
        IUser IUserLogin.Tenant => this.Tenant;

        IChannel ILoginEntity.Channel => this.Channel;
    }
}
