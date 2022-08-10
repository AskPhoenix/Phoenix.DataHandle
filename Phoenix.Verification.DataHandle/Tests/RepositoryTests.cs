using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Main.Models;
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
        public async void FetchUser()
        {
            var userRepo = new UserRepository(_phoenixContext, nonObviatedOnly: true);
            
            var user = await userRepo.FindPrimaryAsync(26);
        }

        [Fact]
        public async void UpdateUser()
        {
            var userRepo = new UserRepository(_phoenixContext, nonObviatedOnly: true);

            var user = await userRepo.FindPrimaryAsync(26);
            user = await userRepo.UpdateAsync(user!);
        }

        [Fact]
        public async void DeleteBook()
        {
            var bookRepository = new BookRepository(_phoenixContext);

            await bookRepository.DeleteAsync(10);

            await bookRepository.DeleteRangeAsync(new int[2] { 11, 12 });
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

            await userRepository.DeleteAsync(25);
        }

        [Fact]
        public async void DeleteSchool()
        {
            var schoolRepository = new SchoolRepository(_phoenixContext);

            await schoolRepository.DeleteAsync(2);
        }

        [Fact]
        public async void FetchDevRegistration()
        {
            var devRegistrationRepository = new DevRegistrationRepository(_phoenixContext);
            var devReg = await devRegistrationRepository.FindUniqueAsync("tspyrou@askphoenix.gr");
        }

        [Fact]
        public async void CreateRangeCourse()
        {
            var courseRepository = new CourseRepository(_phoenixContext);

            var courses = new List<Course>(2)
            {
                new()
                {
                    SchoolId = 1,
                    Code = 1,
                    Name = "A Course",
                    SubCourse = "Sub",
                    Level = "B2",
                    Group = "1",
                    Comments = null,
                    FirstDate = new DateTime(2021, 9, 1),
                    LastDate = new DateTime(2022, 9, 30)
                },

                new()
                {
                    SchoolId = 1,
                    Code = 2,
                    Name = "Another Course",
                    SubCourse = "Sub",
                    Level = "B2",
                    Group = "2",
                    Comments = null,
                    FirstDate = new DateTime(2021, 9, 1),
                    LastDate = new DateTime(2022, 9, 30)
                }
            };

            await courseRepository.CreateRangeAsync(courses);
        }
    }
}
