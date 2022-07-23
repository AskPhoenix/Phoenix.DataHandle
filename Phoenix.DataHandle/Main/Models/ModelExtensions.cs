using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Base;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Models.Extensions;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class PhoenixContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Phoenix");
        }
    }

    public partial class Book : IBook, INormalizableEntity<Book>
    {
        IEnumerable<IExercise> IBook.Exercises => this.Exercises;
        IEnumerable<IMaterial> IBook.Materials => this.Materials;
        
        IEnumerable<ICourse> IBook.Courses => this.Courses;

        public static Func<string, string> NormFunc => s => s.ToUpperInvariant();
        public Book Normalize()
        {
            this.NormalizedName = Book.NormFunc(this.Name);
            
            return this;
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

    public partial class Classroom : IClassroom, IObviableModelEntity, INormalizableEntity<Classroom>
    {
        ISchool IClassroom.School => this.School;

        IEnumerable<ILecture> IClassroom.Lectures => this.Lectures;
        IEnumerable<ISchedule> IClassroom.Schedules => this.Schedules;

        public static Func<string, string> NormFunc => s => s.ToUpperInvariant();
        public Classroom Normalize()
        {
            this.NormalizedName = Classroom.NormFunc(this.Name);

            return this;
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

    public partial class Lecture : ILecture, IObviableModelEntity
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

    public partial class SchoolConnection : ISchoolConnection, IConnectionEntity<SchoolConnection>
    {
        ISchool ISchoolConnection.Tenant => this.Tenant;

        public SchoolConnection Normalize()
        {
            this.Channel = IConnectionEntity<SchoolConnection>.NormFunc(this.Channel);

            return this;
        }
    }

    public partial class School : ISchool, IObviableModelEntity
    {
        ISchoolSetting ISchool.SchoolSetting => this.SchoolSetting;
        IEnumerable<IBroadcast> ISchool.Broadcasts => this.Broadcasts;
        IEnumerable<IClassroom> ISchool.Classrooms => this.Classrooms;
        IEnumerable<ICourse> ISchool.Courses => this.Courses;
        IEnumerable<ISchoolConnection> ISchool.SchoolConnections => this.SchoolConnections;

        IEnumerable<IUser> ISchool.Users => this.Users;
    }

    public partial class SchoolSetting : ISchoolSetting
    {
        ISchool ISchoolSetting.School => this.School;
    }

    public partial class UserConnection : IUserConnection, IConnectionEntity<UserConnection>
    {
        IUser IUserConnection.Tenant => this.Tenant;

        public UserConnection Normalize()
        {
            this.Channel = IConnectionEntity<UserConnection>.NormFunc(this.Channel);

            return this;
        }
    }

    public partial class User : IUser, IObviableModelEntity
    {
        public string FullName => this.BuildFullName();

        int IModelEntity.Id => this.AspNetUserId;

        IEnumerable<IBotFeedback> IUser.BotFeedbacks => this.BotFeedbacks;
        IEnumerable<IBroadcast> IUser.Broadcasts => this.Broadcasts;
        IEnumerable<IGrade> IUser.Grades => this.Grades;
        IEnumerable<IOneTimeCode> IUser.OneTimeCodes => this.OneTimeCodes;
        IEnumerable<IUserConnection> IUser.UserConnections => this.UserConnections;

        IEnumerable<IUser> IUser.Children => this.Children;
        IEnumerable<ICourse> IUser.Courses => this.Courses;
        IEnumerable<ILecture> IUser.Lectures => this.Lectures;
        IEnumerable<IUser> IUser.Parents => this.Parents;
        IEnumerable<ISchool> IUser.Schools => this.Schools;
    }
}
