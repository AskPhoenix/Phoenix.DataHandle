using Microsoft.Extensions.Configuration;
using Phoenix.DataHandle.DataEntry;
using Phoenix.DataHandle.DataEntry.Models;
using Phoenix.DataHandle.DataEntry.Models.Extensions;
using Phoenix.DataHandle.DataEntry.Models.Uniques;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Phoenix.DataHandle.Tests
{
    public class DataEntryTests
    {
        public IConfiguration _configuration { get; }
        private bool IsAuthenticated { get; set; }

        private readonly string WPUsername;
        private readonly string WPPassword;

        public DataEntryTests()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            WPUsername = _configuration["WordPressAuth:Username"];
            WPPassword = _configuration["WordPressAuth:Password"];
        }

        [Fact]
        public async Task AuthenticationTestAsync()
        {
            if (IsAuthenticated)
                return;

            IsAuthenticated = await WPClientWrapper.AuthenticateAsync(WPUsername, WPPassword);
            WPClientWrapper.AlwaysUseAuthentication = true;

            Assert.True(IsAuthenticated);
        }

        [Fact]
        public async void CategoriesTestAsync()
        {
            await AuthenticationTestAsync();

            var categories = await WPClientWrapper.GetCategoriesAsync();
            var postCategories = Enum.GetValues<PostCategory>();

            foreach (var postCategory in postCategories)
            {
                int id = await WPClientWrapper.GetCategoryIdAsync(postCategory);
                Assert.Contains(categories, c => c.Id == id);
            }
        }

        [Fact]
        public async void PostsTestAsync()
        {
            await AuthenticationTestAsync();

            var postCatInt = Enum.GetValues<PostCategory>().Cast<int>();
            var randomPostCat = (PostCategory)new Random().Next(postCatInt.Min(), postCatInt.Max());

            int catId = await WPClientWrapper.GetCategoryIdAsync(randomPostCat);
            var posts = await WPClientWrapper.GetPostsPageAsync(1, randomPostCat);

            Assert.InRange(posts.Count(), 0, WPClientWrapper.PostsPerPage);
            Assert.All(posts, p => p.Categories.Contains(catId));

            var schoolPosts = await WPClientWrapper.GetPostsAsync(PostCategory.SchoolInformation);

            // TODO: Test when WP import is updated to use the school code in the post title

            return;

            var filteredSchoolPosts = schoolPosts.FilterPostsForSchool(new SchoolUnique(1));

            Assert.Single(filteredSchoolPosts);
            Assert.Equal(new SchoolUnique(1), new SchoolUnique(filteredSchoolPosts.Single().GetTitle()));
        }

        private async Task AcfTestAsync<TAcf>(PostCategory cat)
            where TAcf : IModelAcf
        {
            await AuthenticationTestAsync();

            WPClientWrapper.PostsPerPage = 1;

            var posts = await WPClientWrapper.GetPostsPageAsync(1, cat);
            Assert.Single(posts);

            var acf = await WPClientWrapper.GetAcfAsync<TAcf>(posts.Single());
            Assert.NotNull(acf);

            // TODO: Test when WP import is updated to use the school code in the post title

            return;

            var posts2 = await WPClientWrapper.GetPostsPageAsync(1, cat, new SchoolUnique(1));
            Assert.Single(posts2);

            var acf2 = await WPClientWrapper.GetAcfAsync<TAcf>(posts2.Single());
            Assert.NotNull(acf2);
        }

        [Fact]
        public async void SchoolAcfTestAsync()
        {
            await AcfTestAsync<SchoolAcf>(PostCategory.SchoolInformation);
        }

        [Fact]
        public async void CourseAcfTestAsync()
        {
            await AcfTestAsync<CourseAcf>(PostCategory.Course);
        }

        [Fact]
        public async void ScheduleAcfTestAsync()
        {
            await AcfTestAsync<ScheduleAcf>(PostCategory.Schedule);
        }

        [Fact]
        public async void PersonnelAcfTestAsync()
        {
            await AcfTestAsync<PersonnelAcf>(PostCategory.Personnel);
        }

        [Fact]
        public async void ClientAcfTestAsync()
        {
            await AcfTestAsync<ClientAcf>(PostCategory.Client);
        }
    }
}
