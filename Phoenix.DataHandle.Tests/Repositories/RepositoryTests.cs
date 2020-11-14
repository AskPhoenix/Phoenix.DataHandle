using System;
using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Repositories;
using Xunit;

namespace Phoenix.DataHandle.Tests.Repositories
{
    public class RepositoryTests : IDisposable
    {
        private const string CONNECTION_STRING = "Server=.;Initial Catalog=PhoenixDB;Persist Security Info=False;User ID=sa;Password=root;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=True;Connection Timeout=30;";
        private readonly PhoenixContext _phoenixContext;

        public RepositoryTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<PhoenixContext>();
            optionsBuilder.UseSqlServer(CONNECTION_STRING);
            this._phoenixContext = new PhoenixContext(optionsBuilder.Options);
        }

        public void Dispose()
        {
            this._phoenixContext.Dispose();
        }

        [Fact]
        public async void FetchSchools()
        {
            SchoolRepository schoolRepository = new SchoolRepository(this._phoenixContext);

            _ = await schoolRepository.Find().ToListAsync();
        }

        [Fact]
        public async void FetchAspNetUsers()
        {
            AspNetUserRepository aspNetUserRepository = new AspNetUserRepository(this._phoenixContext);

            _ = await aspNetUserRepository.Find().ToListAsync();
        }

        [Fact]
        public async void FetchCourses()
        {
            CourseRepository courseRepository = new CourseRepository(this._phoenixContext);

            _ = await courseRepository.Find().ToListAsync();
        }

        [Fact]
        public async void FetchLectures()
        {
            LectureRepository lectureRepository = new LectureRepository(this._phoenixContext);

            _ = await lectureRepository.Find().ToListAsync();
        }

        [Fact]
        public async void FetchExams()
        {
            ExamRepository examRepository = new ExamRepository(this._phoenixContext);

            _ = await examRepository.Find().ToListAsync();
        }

        [Fact]
        public async void FetchExercises()
        {
            ExerciseRepository exerciseRepository = new ExerciseRepository(this._phoenixContext);

            _ = await exerciseRepository.Find().ToListAsync();
        }

        [Fact]
        public async void FetchAUsers()
        {
            UserRepository userRepository = new UserRepository(this._phoenixContext);

            _ = await userRepository.Find().ToListAsync();
        }


    }
}
