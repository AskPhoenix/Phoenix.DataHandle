using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Api.Models.Main;
using Phoenix.DataHandle.Identity;
using Phoenix.DataHandle.Main.Models;
using Phoenix.Verification.Base;
using Phoenix.Verification.Utilities;
using System;
using Xunit;

namespace Phoenix.Verification.DataHandle.Tests
{
    public class ApiTests : ContextTestsBase
    {
        private const string OutDirName = "api_tests";

        [Fact]
        public async void UserApiTestAsync()
        {
            var user = await _phoenixContext.UserInfos.Include(u => u.AspNetUser)
                .Include(u => u.Courses)
                .FirstOrDefaultAsync()
                ?? new UserInfo
                {
                    FirstName = "Kapoios",
                    LastName = "Kapoiou",
                    HasAcceptedTerms = true,
                    IsSelfDetermined = true,

                    AspNetUser = new ApplicationUser
                    {
                        PhoneNumber = "0000000000",
                        Email = "kati@kapou.com",
                        UserName = "kapoios"
                    }
                };

            var userApi = new UserInfoApi(user);

            JsonUtilities.SaveToFile(userApi, OutDirName, nameof(user));
            var user2 = JsonUtilities.ReadFromFile<UserInfoApi>(OutDirName, nameof(user));
        }

        [Fact]
        public async void BookApiTestAsync()
        {
            var book = await _phoenixContext.Books.FirstOrDefaultAsync()
                ?? new Book
                {
                    Name = "Biblio",
                    NormalizedName = "BIBLIO",
                    Publisher = "Oikos"
                };

            var bookApi = new BookApi(book);

            JsonUtilities.SaveToFile(bookApi, OutDirName, nameof(book));
            var bookApi2 = JsonUtilities.ReadFromFile<BookApi>(OutDirName, nameof(book));
        }

        [Fact]
        public async void ClassroomApiTestAsync()
        {
            var classroom = await _phoenixContext.Classrooms.Include(c => c.School)
                .FirstOrDefaultAsync()
                ?? new Classroom
                {
                    Name = "Taxi",
                    NormalizedName = "TAXI"
                };

            var classroomApi = new ClassroomApi(classroom, include: true);

            JsonUtilities.SaveToFile(classroomApi, OutDirName, nameof(classroom));
            var classroomApi2 = JsonUtilities.ReadFromFile<ClassroomApi>(OutDirName, nameof(classroom));
        }

        [Fact]
        public async void CourseApiTestAsync()
        {
            var course = await _phoenixContext.Courses.Include(c => c.Grades)
                .Include(c => c.Lectures)
                .Include(c => c.Schedules)
                .Include(c => c.Users)
                .Include(c => c.Books)
                .Include(c => c.School)
                .FirstOrDefaultAsync()
                ?? new Course
                {
                    Name = "Mathima",
                    Code = 3,
                    FirstDate = DateTime.Now,
                    LastDate = DateTime.Now.AddMonths(5),
                    Group = "5",
                    Level = "B1"
                };

            var courseApi = new CourseApi(course, include: true);

            JsonUtilities.SaveToFile(courseApi, OutDirName, nameof(course));
            var courseApi2 = JsonUtilities.ReadFromFile<CourseApi>(OutDirName, nameof(course));
        }

        [Fact]
        public async void ExamApiTestAsync()
        {
            var exam = await _phoenixContext.Exams.Include(e => e.Grades)
                .Include(e => e.Materials)
                .Include(e => e.Lecture)
                .FirstOrDefaultAsync()
                ?? new Exam
                {
                    Name = "Exetasi",
                    Comments = "Kati"
                };

            var examApi = new ExamApi(exam, include: true);

            JsonUtilities.SaveToFile(examApi, OutDirName, nameof(exam));
            var examApi2 = JsonUtilities.ReadFromFile<ExamApi>(OutDirName, nameof(exam));
        }

        [Fact]
        public async void ExerciseApiTestAsync()
        {
            var exercise = await _phoenixContext.Exercises.Include(e => e.Grades)
                .Include(e => e.Lecture)
                .Include(e => e.Book)
                .FirstOrDefaultAsync()
                ?? new Exercise
                {
                    Name = "Askisi",
                    Page = "4",
                    Comments = "Sxolio"
                };

            var exerciseApi = new ExerciseApi(exercise, include: true);

            JsonUtilities.SaveToFile(exerciseApi, OutDirName, nameof(exercise));
            var exerciseApi2 = JsonUtilities.ReadFromFile<ExerciseApi>(OutDirName, nameof(exercise));
        }

        [Fact]
        public async void GradeApiTestAsync()
        {
            var grade = await _phoenixContext.Grades.Include(g => g.Student)
                .Include(g => g.Course)
                .Include(g => g.Exam)
                .Include(g => g.Exercise)
                .FirstOrDefaultAsync()
                ?? new Grade
                {
                    Score = 20,
                    Justification = "Arista",
                    Topic = "B' Trimino"
                };

            var gradeApi = new GradeApi(grade, include: true);

            JsonUtilities.SaveToFile(gradeApi, OutDirName, nameof(grade));
            var exerciseApi2 = JsonUtilities.ReadFromFile<GradeApi>(OutDirName, nameof(grade));
        }

        [Fact]
        public async void LectureApiTestAsync()
        {
            var lecture = await _phoenixContext.Lectures.Include(l => l.Exams)
                .Include(l => l.Exercises)
                .Include(l => l.Course)
                .Include(l => l.Classroom)
                .Include(l => l.Schedule)
                .FirstOrDefaultAsync()
                ?? new Lecture
                {
                    StartDateTime = DateTime.Now,
                    EndDateTime = DateTime.Now.AddHours(2)
                };

            var lectureApi = new LectureApi(lecture, include: true);

            JsonUtilities.SaveToFile(lectureApi, OutDirName, nameof(lecture));
            var lectureApi2 = JsonUtilities.ReadFromFile<LectureApi>(OutDirName, nameof(lecture));
        }

        [Fact]
        public async void MaterialApiTestAsync()
        {
            var material = await _phoenixContext.Materials.Include(m => m.Exam)
                .Include(m => m.Book)
                .FirstOrDefaultAsync()
                ?? new Material
                {
                    Chapter = "II",
                    Section = "A",
                    Comments = "Yli"
                };

            var materialApi = new MaterialApi(material, include: true);

            JsonUtilities.SaveToFile(materialApi, OutDirName, nameof(material));
            var materialApi2 = JsonUtilities.ReadFromFile<LectureApi>(OutDirName, nameof(material));
        }

        [Fact]
        public async void ScheduleApiTestAsync()
        {
            var schedule = await _phoenixContext.Schedules.Include(s => s.Course)
                .Include(s => s.Classroom)
                .FirstOrDefaultAsync()
                ?? new Schedule
                {
                    DayOfWeek = DayOfWeek.Friday,
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddHours(2)
                };

            var scheduleApi = new ScheduleApi(schedule, include: true);

            JsonUtilities.SaveToFile(scheduleApi, OutDirName, nameof(schedule));
            var scheduleApi2 = JsonUtilities.ReadFromFile<ScheduleApi>(OutDirName, nameof(schedule));
        }

        [Fact]
        public async void SchoolApiTestAsync()
        {
            var school = await _phoenixContext.Schools.Include(s => s.Classrooms)
                .Include(s => s.Courses)
                .Include(s => s.SchoolInfo)
                .FirstOrDefaultAsync()
                ?? new School
                {
                    Name = "Sxoleio",
                    Slug = "Sxoleio",
                    City = "Poli",
                    AddressLine = "Dieuthinsi",
                    SchoolInfo = new SchoolInfo
                    {
                        Country = "Ellada",
                        PhoneCountryCode = "0030",
                        PrimaryLanguage = "Greek",
                        PrimaryLocale = "el-GR",
                        SecondaryLanguage = "English",
                        SecondaryLocale = "en-US",
                        TimeZone = "Eastern European Time"
                    }
                };

            var schoolApi = new SchoolApi(school, include: true);

            JsonUtilities.SaveToFile(schoolApi, OutDirName, nameof(school));
            var schoolApi2 = JsonUtilities.ReadFromFile<SchoolApi>(OutDirName, nameof(school));
        }
    }
}
