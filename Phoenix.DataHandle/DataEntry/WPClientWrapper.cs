using Newtonsoft.Json.Linq;
using Phoenix.DataHandle.DataEntry.Models;
using Phoenix.DataHandle.DataEntry.Models.Extensions;
using Phoenix.DataHandle.DataEntry.Models.Uniques;
using Phoenix.DataHandle.DataEntry.Types;
using WordPressPCL;
using WordPressPCL.Models;
using WordPressPCL.Models.Exceptions;
using WordPressPCL.Utility;

namespace Phoenix.DataHandle.DataEntry
{
    public static class WPClientWrapper
    {
        private const string WordpressEndpoint = "https://www.askphoenix.gr/";
        private const string PostsPath = "wp/v2/posts";
        private const string AcfPostsPath = "acf/v3/posts";

        private static WordPressClient Client { get; }
        public static bool AlwaysUseAuthentication { get; set; }
        public static bool IsAuthenticated { get; private set; } // => !string.IsNullOrEmpty(Client.Auth.GetToken());
        public static bool Embed { get; set; }
        public static int PostsPerPage { get; set; } = 10;

        static WPClientWrapper()
        {
            Client = new WordPressClient(new Uri(new Uri(WordpressEndpoint), "wp-json/"));
            Client.Auth.UseBearerAuth(JWTPlugin.JWTAuthByEnriqueChavez);
        }

        public static async Task<bool> AuthenticateAsync(string username, string password)
        {
            if (IsAuthenticated)
                return true;

            await Client.Auth.RequestJWTokenAsync(username, password);
            
            IsAuthenticated = await Client.Auth.IsValidJWTokenAsync();
            if (!IsAuthenticated)
                throw new WPException($"Cannot authenticate user '{username}' in WordPress because of invalid JWToken.");

            return IsAuthenticated;
        }

        public static async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await Client.Categories.GetAllAsync(Embed, AlwaysUseAuthentication);
        }

        public static async Task<int> GetCategoryIdAsync(PostCategory category)
        {
            CategoriesQueryBuilder categoriesQueryBuilder = new() 
            {
                Search = category.GetName(),
                Embed = Embed 
            };
            
            var matches = await Client.Categories.QueryAsync(categoriesQueryBuilder, AlwaysUseAuthentication);
            
            return matches.Single(c => c.Name == category.GetName()).Id;
        }

        public static async Task<IEnumerable<Post>> GetPostsPageAsync(PostsQueryBuilder queryBuilder)
        {
            queryBuilder.PerPage = PostsPerPage;
            queryBuilder.Embed = Embed;

            string route = PostsPath + queryBuilder.BuildQuery();

            IEnumerable<Post> posts;
            try
            {
                posts = await GetCustomAsync<IEnumerable<Post>>(route);
                posts = posts.Where(p => p.Status == Status.Publish);
            }
            catch (Exception)
            {
                posts = Enumerable.Empty<Post>();
            }

            return posts;
        }

        public static async Task<IEnumerable<Post>> GetPostsPageAsync(int page, int categoryId, string search)
        {
            var queryBuilder = new PostsQueryBuilder()
            {
                Page = page,
                Categories = new List<int>(1) { categoryId },
                Search = search
            };

            return await GetPostsPageAsync(queryBuilder);
        }

        public static async Task<IEnumerable<Post>> GetPostsPageAsync(int page, PostCategory category, string search)
        {
            int categoryId = await GetCategoryIdAsync(category);
            return await GetPostsPageAsync(page, categoryId, search);
        }

        public static async Task<IEnumerable<Post>> GetPostsPageAsync(int page, PostCategory category, BusinessUnique uq)
        {
            int categoryId = await GetCategoryIdAsync(category);
            return await GetPostsPageAsync(page, categoryId, uq.ToString());
        }

        public static async Task<IEnumerable<Post>> GetPostsPageAsync(int page, int categoryId)
        {
            return await GetPostsPageAsync(page, categoryId, null!);
        }

        public static async Task<IEnumerable<Post>> GetPostsPageAsync(int page, PostCategory category)
        {
            int categoryId = await GetCategoryIdAsync(category);
            return await GetPostsPageAsync(page, categoryId);
        }

        public static async Task<IEnumerable<Post>> GetPostsPageAsync(int page, string search)
        {
            var queryBuilder = new PostsQueryBuilder()
            {
                Page = page,
                Search = search
            };

            return await GetPostsPageAsync(queryBuilder);
        }

        public static async Task<IEnumerable<Post>> GetPostsPageAsync(int page, BusinessUnique uq)
        {
            return await GetPostsAsync(page, uq.ToString());
        }

        public static async Task<IEnumerable<Post>> GetPostsAsync(PostsQueryBuilder queryBuilder)
        {
            List<Post> posts = new();
            IEnumerable<Post> nextPosts;

            queryBuilder.Page = 1;
            queryBuilder.PerPage = PostsPerPage;
            queryBuilder.Embed = Embed;

            do
            {
                nextPosts = await GetPostsPageAsync(queryBuilder);

                if (!nextPosts.Any())
                    break;

                posts.AddRange(nextPosts);

                queryBuilder.Page++;
            } while (nextPosts.Count() % PostsPerPage == 0);

            return posts;
        }

        public static async Task<IEnumerable<Post>> GetPostsAsync(int categoryId, string search)
        {
            var queryBuilder = new PostsQueryBuilder()
            {
                Categories = new List<int>(1) { categoryId },
                Search = search
            };

            return await GetPostsAsync(queryBuilder);
        }

        public static async Task<IEnumerable<Post>> GetPostsAsync(PostCategory category, string search)
        {
            int categoryId = await GetCategoryIdAsync(category);
            return await GetPostsAsync(categoryId, search);
        }

        public static async Task<IEnumerable<Post>> GetPostsAsync(int categoryId)
        {
            return await GetPostsAsync(categoryId, null!);
        }

        public static async Task<IEnumerable<Post>> GetPostsAsync(PostCategory category)
        {
            int categoryId = await GetCategoryIdAsync(category);
            return await GetPostsAsync(categoryId);
        }

        public static async Task<IEnumerable<Post>> GetPostsAsync(string search)
        {
            return await GetPostsAsync(new PostsQueryBuilder() { Search = search });
        }

        public static IEnumerable<Post> FilterPostsForSchool(this IEnumerable<Post> posts, SchoolUnique schoolUnique)
        {
            return posts.Where(p => schoolUnique.Equals(new SchoolUnique(p.GetTitle())));
        }

        public static async Task<T> GetCustomAsync<T>(string route)
            where T : class
        {
            return await Client.CustomRequest.GetAsync<T>(route, Embed, AlwaysUseAuthentication);
        }

        public static async Task<TModelACF> GetAcfAsync<TModelACF>(int postId)
            where TModelACF : IModelAcf
        {
            string route = AcfPostsPath + $"/{postId}";
            var response = await GetCustomAsync<JObject>(route);

            return response.GetValue("acf").ToObject<TModelACF>();
        }

        public static async Task<TModelACF> GetAcfAsync<TModelACF>(Post post)
            where TModelACF : IModelAcf
        {
            return await GetAcfAsync<TModelACF>(post.Id);
        }

        public static async Task<SchoolAcf> GetSchoolAcfAsync(Post post)
        {
            SchoolAcf schoolAcf = await GetAcfAsync<SchoolAcf>(post);
            schoolAcf.Code = new SchoolUnique(post.GetTitle()).Code;

            return schoolAcf;
        }

        public static async Task<CourseAcf> GetCourseAcfAsync(int postId)
        {
            return await GetAcfAsync<CourseAcf>(postId);
        }

        public static async Task<CourseAcf> GetCourseAcfAsync(Post post)
        {
            return await GetAcfAsync<CourseAcf>(post);
        }

        public static async Task<ScheduleAcf> GetScheduleAcfAsync(int postId)
        {
            return await GetAcfAsync<ScheduleAcf>(postId);
        }

        public static async Task<ScheduleAcf> GetScheduleAcfAsync(Post post)
        {
            return await GetAcfAsync<ScheduleAcf>(post);
        }

        public static async Task<PersonnelAcf> GetPersonnelAcfAsync(int postId)
        {
            return await GetAcfAsync<PersonnelAcf>(postId);
        }

        public static async Task<PersonnelAcf> GetPersonnelAcfAsync(Post post)
        {
            return await GetAcfAsync<PersonnelAcf>(post);
        }

        public static async Task<ClientAcf> GetClientAcfAsync(int postId)
        {
            return await GetAcfAsync<ClientAcf>(postId);
        }

        public static async Task<ClientAcf> GetClientAcfAsync(Post post)
        {
            return await GetAcfAsync<ClientAcf>(post);
        }

        public static async Task<bool> DeletePostAsync(int postId)
        {
            return await Client.Posts.DeleteAsync(postId);
        }

        public static async Task<bool> DeletePostAsync(Post post)
        {
            return await DeletePostAsync(post.Id);
        }
    }
}
