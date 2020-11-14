using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Phoenix.DataHandle.Main.Models;
using Phoenix.DataHandle.Repositories;
using Phoenix.DataHandle.Services;
using Xunit;

namespace Phoenix.DataHandle.Tests.Services
{
    public class LectureServiceTests : IDisposable
    {
        private const string CONNECTION_STRING = "Server=.;Initial Catalog=PhoenixDB;Persist Security Info=False;User ID=sa;Password=root;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=True;Connection Timeout=30;";
        private readonly PhoenixContext _phoenixContext;

        public LectureServiceTests()
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
            Repository<Course> courseRepository = new Repository<Course>(this._phoenixContext);
            courseRepository.Include(a => a.Include(b => b.School).Include(b => b.Lecture).Include(b => b.Schedule).ThenInclude(b => b.Classroom).Include(b => b.Schedule).ThenInclude(b => b.Course));

            ICollection<Course> courses = await courseRepository.Find().ToListAsync();
            courses = courses.OrderBy(x => Guid.NewGuid()).Take(2).ToList();

            LectureService lectureService = new LectureService(this._phoenixContext, NullLogger<LectureService>.Instance);

            await lectureService.generateLectures(courses, CancellationToken.None);
        }



    }
}
