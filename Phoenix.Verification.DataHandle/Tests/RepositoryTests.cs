using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Repositories;
using Phoenix.Verification.Base;
using Xunit;

namespace Phoenix.Verification.DataHandle.Tests
{
    public class RepositoryTests : ContextTestsBase
    {
        // TODO: Create Tests after filling the DB

        public RepositoryTests()
            : base()
        {
        }

        [Fact]
        public async void FetchSchools()
        {
            var schoolRepo = new SchoolRepository(_phoenixContext);

            await schoolRepo.Find().ToListAsync();
        }
    }
}
