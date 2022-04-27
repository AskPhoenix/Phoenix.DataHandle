using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Identity;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models.Extensions;
using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class PhoenixContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasOne(d => d.AspNetUser)
                    .WithOne(p => p.UserInfo)
                    .HasForeignKey<UserInfo>(d => d.AspNetUserId)
                    .HasConstraintName("FK_Users_AspNetUsers");
            });
        }
    }

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
        IUserInfo IBotFeedback.Author => this.Author;
    }

    public partial class Broadcast : IBroadcast, IModelEntity
    {
        ISchool IBroadcast.School => this.School;
        IUserInfo IBroadcast.Author => this.Author;

        IEnumerable<ICourse> IBroadcast.Courses => this.Courses;
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
        IEnumerable<IUserInfo> ICourse.Users => this.Users;
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
        IUserInfo IGrade.Student => this.Student;
        ICourse? IGrade.Course => this.Course;
        IExam? IGrade.Exam => this.Exam;
        IExercise? IGrade.Exercise => this.Exercise;
    }

    public partial class Lecture : ILecture, IObviableModelEntity
    {
        ICourse ILecture.Course => this.Course;
        IClassroom? ILecture.Classroom => this.Classroom;
        ISchedule? ILecture.Schedule => this.Schedule;
        ILecture? ILecture.ReplacementLecture => this.ReplacementLecture;

        IEnumerable<IExam> ILecture.Exams => this.Exams;
        IEnumerable<IExercise> ILecture.Exercises => this.Exercises;
        IEnumerable<ILecture> ILecture.InverseReplacementLecture => this.InverseReplacementLecture;

        IEnumerable<IUserInfo> ILecture.Attendees => this.Attendees;
    }

    public partial class Material : IMaterial, IModelEntity
    {
        IExam IMaterial.Exam => this.Exam;
        IBook? IMaterial.Book => this.Book;
    }

    public partial class OneTimeCode : IOneTimeCode, IModelEntity
    {
        IUserInfo IOneTimeCode.User => this.User;
    }
    
    public partial class Schedule : ISchedule, IObviableModelEntity
    {
        ICourse ISchedule.Course => this.Course;
        IClassroom? ISchedule.Classroom => this.Classroom;

        IEnumerable<ILecture> ISchedule.Lectures => this.Lectures;
    }

    public partial class SchoolConnection : ISchoolConnection, IConnectionEntity
    {
        ISchool ISchoolConnection.Tenant => this.Tenant;
    }

    public partial class School : ISchool, IObviableModelEntity
    {
        ISchoolSetting ISchool.SchoolSetting => this.SchoolSetting;
        IEnumerable<IBroadcast> ISchool.Broadcasts => this.Broadcasts;
        IEnumerable<IClassroom> ISchool.Classrooms => this.Classrooms;
        IEnumerable<ICourse> ISchool.Courses => this.Courses;
        IEnumerable<ISchoolConnection> ISchool.SchoolConnections => this.SchoolConnections;

        IEnumerable<IUserInfo> ISchool.Users => this.Users;
    }

    public partial class SchoolSetting : ISchoolSetting
    {
        ISchool ISchoolSetting.School => this.School;
    }

    public partial class UserConnection : IUserConnection, IConnectionEntity
    {
        IUserInfo IUserConnection.Tenant => this.Tenant;
    }

    public partial class UserInfo : IUserInfo, IObviableModelEntity
    {
        public ApplicationUser AspNetUser { get; set; } = null!;
        public string FullName => this.BuildFullName();

        int IModelEntity.Id => this.AspNetUserId;

        IAspNetUser IUserInfo.AspNetUser => this.AspNetUser;

        IEnumerable<IBotFeedback> IUserInfo.BotFeedbacks => this.BotFeedbacks;
        IEnumerable<IBroadcast> IUserInfo.Broadcasts => this.Broadcasts;
        IEnumerable<IGrade> IUserInfo.Grades => this.Grades;
        IEnumerable<IOneTimeCode> IUserInfo.OneTimeCodes => this.OneTimeCodes;
        IEnumerable<IUserConnection> IUserInfo.UserConnections => this.UserConnections;

        IEnumerable<IUserInfo> IUserInfo.Children => this.Children;
        IEnumerable<ICourse> IUserInfo.Courses => this.Courses;
        IEnumerable<ILecture> IUserInfo.Lectures => this.Lectures;
        IEnumerable<IUserInfo> IUserInfo.Parents => this.Parents;
        IEnumerable<ISchool> IUserInfo.Schools => this.Schools;
    }
}
