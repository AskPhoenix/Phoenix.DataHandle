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

            _ = await schoolRepository.find().ToListAsync();
        }

        [Fact]
        public async void FetchLectures()
        {
            LectureRepository lectureRepository = new LectureRepository(this._phoenixContext);

            _ = await lectureRepository.find().ToListAsync();
        }


    }
}
