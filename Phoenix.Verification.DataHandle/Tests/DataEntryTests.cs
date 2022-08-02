using Phoenix.DataHandle.DataEntry;
using Phoenix.DataHandle.DataEntry.Models;
using Phoenix.DataHandle.DataEntry.Models.Extensions;
using Phoenix.DataHandle.DataEntry.Types;
using Phoenix.DataHandle.DataEntry.Types.Uniques;
using Phoenix.Verification.Utilities;

namespace Phoenix.Verification.DataHandle.Tests
{
    public class DataEntryTests : ConfigurationTestsBase
    {
        private readonly string WPUsername;
        private readonly string WPPassword;

        private const string OutDirName = "dataentry_tests";

        private bool IsAuthenticated { get; set; }

        public DataEntryTests()
            : base()
        {
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
        public async void DeletePostsAsync()
        {
            await AuthenticationTestAsync();

            var posts = await WPClientWrapper.GetPostsAsync(PostCategory.Personnel);

            foreach (var post in posts)
                await WPClientWrapper.DeletePostAsync(post);

            posts = await WPClientWrapper.GetPostsAsync(PostCategory.Personnel);

            Assert.Empty(posts);
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

            var filteredSchoolPosts = schoolPosts.FilterPostsForSchool(new SchoolUnique(1));
            int ps = filteredSchoolPosts.Count();

            Assert.Single(filteredSchoolPosts);
            Assert.Equal(new SchoolUnique(1), new SchoolUnique(filteredSchoolPosts.Single().GetTitle()));

            var coursePosts = await WPClientWrapper.GetPostsAsync(PostCategory.Course);

            var filteredCoursePosts = coursePosts.FilterPostsForSchool(new SchoolUnique(1));
            int pc = filteredCoursePosts.Count();

            var courseUqs = filteredCoursePosts.Select(p => new CourseUnique(p.GetTitle()));
        }

        private async Task<TAcf> AcfTestAsync<TAcf>(PostCategory cat)
            where TAcf : IModelAcf
        {
            await AuthenticationTestAsync();

            WPClientWrapper.PostsPerPage = 1;

            var posts = await WPClientWrapper.GetPostsPageAsync(1, cat);
            Assert.Single(posts);

            var acf = await WPClientWrapper.GetAcfAsync<TAcf>(posts.Single());
            Assert.NotNull(acf);

            JsonUtilities.SaveToFile(acf, OutDirName, cat.ToString());

            var posts2 = await WPClientWrapper.GetPostsPageAsync(1, cat, new SchoolUnique(1));
            Assert.Single(posts2);

            var acf2 = await WPClientWrapper.GetAcfAsync<TAcf>(posts2.Single());
            Assert.NotNull(acf2);

            return acf2;
        }

        [Fact]
        public async void SchoolAcfTestAsync()
        {
            var acf = await AcfTestAsync<SchoolAcf>(PostCategory.SchoolInformation);
        }

        [Fact]
        public async void CourseAcfTestAsync()
        {
            var acf = await AcfTestAsync<CourseAcf>(PostCategory.Course);
            var books = acf.Books;
        }

        [Fact]
        public async void ScheduleAcfTestAsync()
        {
            var acf = await AcfTestAsync<ScheduleAcf>(PostCategory.Schedule);
        }

        [Fact]
        public async void PersonnelAcfTestAsync()
        {
            var acf = await AcfTestAsync<PersonnelAcf>(PostCategory.Personnel);
        }

        [Fact]
        public async void ClientAcfTestAsync()
        {
            var acf = await AcfTestAsync<ClientAcf>(PostCategory.Client);
        }
    }
}
