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
            var schoolRepo = new SchoolRepository(_phoenixContext, nonObviatedOnly: true);

            await schoolRepo.Find().ToListAsync();
        }

        [Fact]
        public async void DeleteBook()
        {
            var bookRepository = new BookRepository(_phoenixContext);

            await bookRepository.DeleteAsync(9);

            await bookRepository.DeleteRangeAsync(new int[2] { 7, 8 });
        }

        [Fact]
        public async void DeleteBotFeedback()
        {
            var botFeedbackRepository = new BotFeedbackRepository(_phoenixContext);

            await botFeedbackRepository.DeleteAsync(1);
        }

        [Fact]
        public async void DeleteUser()
        {
            var userRepository = new UserRepository(_phoenixContext);

            await userRepository.DeleteAsync(26);
        }
    }
}
