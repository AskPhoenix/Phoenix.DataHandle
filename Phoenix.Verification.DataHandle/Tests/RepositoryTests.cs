using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Repositories;

namespace Phoenix.Verification.DataHandle.Tests
{
    public class RepositoryTests : ContextTestsBase
    {
        // TODO: Create Tests

        public RepositoryTests()
            : base()
        {
        }

        [Fact]
        public async void FetchSchools()
        {
            var schoolRepo = new SchoolRepository(_phoenixContext, true);

            await schoolRepo.Find().ToListAsync();
        }
    }
}
