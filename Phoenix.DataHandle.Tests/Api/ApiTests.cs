using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Models.Extensions;
using Phoenix.DataHandle.Api.Models.Main;
using Phoenix.DataHandle.Main.Models;
using System;
using System.IO;
using Xunit;

namespace Phoenix.DataHandle.Tests.Api
{
    public class ApiTests : IDisposable
    {
        private const string CONNECTION_STRING = "Server=localhost;Database=PhoenicopterusDB;Trusted_Connection=True;";
        
        private readonly PhoenixContext _phoenixContext;

        public ApiTests()
        {
            _phoenixContext = new PhoenixContext(new DbContextOptionsBuilder<PhoenixContext>().UseSqlServer(CONNECTION_STRING).Options);
        }
        public void Dispose()
        {
            _phoenixContext.Dispose();
        }

        private static void SaveJson(IModelApi modelApi, string filename = "test")
        {
            Directory.CreateDirectory("api_tests");

            string json = JsonConvert.SerializeObject(modelApi, Formatting.Indented);
            File.WriteAllText($"api_tests/{filename}.json", json);
        }

        private static IModelApi ReadJson<ModelApiT>(string filename = "test")
            where ModelApiT : IModelApi
        {
            string json = File.ReadAllText($"api_tests/{filename}.json");
            return JsonConvert.DeserializeObject<ModelApiT>(json);
        }

        [Fact]
        public async void AspNetUserApiTest()
        {
            var aspNetUser = await _phoenixContext.AspNetUsers.Include(u => u.User)
                .Include(u => u.Courses)
                .FirstOrDefaultAsync()
                ?? new AspNetUser
                {
                    PhoneNumber = "0000000000",
                    Email = "kati@kapou.com",
                    UserName = "kapoios",
                    User = new User
                    {
                        FirstName = "Kapoios",
                        LastName = "Kapoiou",
                        TermsAccepted = true,
                        IsSelfDetermined = true
                    }
                };

            var aspNetUserApi = new AspNetUserApi(aspNetUser);

            SaveJson(aspNetUserApi, nameof(aspNetUser));
            var aspNetUser2 = ReadJson<AspNetUserApi>(nameof(aspNetUser));
        }

        [Fact]
        public async void BookApiTest()
        {
            var book = await _phoenixContext.Books.FirstOrDefaultAsync()
                ?? new Book
                {
                    Name = "Biblio",
                    NormalizedName = "BIBLIO",
                    Publisher = "Oikos"
                };

            var bookApi = new BookApi(book);

            SaveJson(bookApi, nameof(book));
            var bookApi2 = ReadJson<BookApi>(nameof(book));
        }

        [Fact]
        public async void ClassroomApiTest()
        {
            var classroom = await _phoenixContext.Classrooms.Include(c => c.School)
                .FirstOrDefaultAsync()
                ?? new Classroom
                {
                    Name = "Taxi",
                    NormalizedName = "TAXI"
                };

            var classroomApi = new ClassroomApi(classroom, include: true);

            SaveJson(classroomApi, nameof(classroom));
            var classroomApi2 = ReadJson<ClassroomApi>(nameof(classroom));
        }

        [Fact]
        public async void CourseApiTest()
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

            SaveJson(courseApi, nameof(course));
            var courseApi2 = ReadJson<CourseApi>(nameof(course));
        }

        [Fact]
        public async void ExamApiTest()
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

            SaveJson(examApi, nameof(exam));
            var examApi2 = ReadJson<ExamApi>(nameof(exam));
        }

        [Fact]
        public async void ExerciseApiTest()
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

            SaveJson(exerciseApi, nameof(exercise));
            var exerciseApi2 = ReadJson<ExerciseApi>(nameof(exercise));
        }

        [Fact]
        public async void GradeApiTest()
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

            SaveJson(gradeApi, nameof(grade));
            var exerciseApi2 = ReadJson<GradeApi>(nameof(grade));
        }

        [Fact]
        public async void LectureApiTest()
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
                    EndDateTime = DateTime.Now.AddHours(2),
                    Status = Main.LectureStatus.Scheduled
                };

            var lectureApi = new LectureApi(lecture, include: true);

            SaveJson(lectureApi, nameof(lecture));
            var lectureApi2 = ReadJson<LectureApi>(nameof(lecture));
        }

        [Fact]
        public async void MaterialApiTest()
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

            SaveJson(materialApi, nameof(material));
            var materialApi2 = ReadJson<LectureApi>(nameof(material));
        }

        [Fact]
        public async void ScheduleApiTest()
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

            SaveJson(scheduleApi, nameof(schedule));
            var scheduleApi2 = ReadJson<ScheduleApi>(nameof(schedule));
        }

        [Fact]
        public async void SchoolApiTest()
        {
            var school = await _phoenixContext.Schools.Include(s => s.Classrooms)
                .Include(s => s.Courses)
                .Include(s => s.SchoolInfo)
                .FirstOrDefaultAsync()
                ?? new School
                {
                    Name = "Sxoleio",
                    Slug = "Sxoleio",
                    NormalizedName = "SXOLEIO",
                    City = "Poli",
                    NormalizedCity = "POLI",
                    AddressLine = "Dieuthinsi",
                    SchoolInfo = new SchoolInfo
                    {
                        Country = "Ellada",
                        PhoneCode = "0030",
                        PrimaryLanguage = "Greek",
                        PrimaryLocale = "el-GR",
                        SecondaryLanguage = "English",
                        SecondaryLocale = "en-US",
                        TimeZone = "Eastern European Time"
                    }
                };

            var schoolApi = new SchoolApi(school, include: true);

            SaveJson(schoolApi, nameof(school));
            var schoolApi2 = ReadJson<SchoolApi>(nameof(school));
        }
    }
}
