using Newtonsoft.Json.Linq;
using Phoenix.DataHandle.DataEntry.Models.Extensions;
using Phoenix.DataHandle.DataEntry.Models.Uniques;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private const int PostsPerPage = 10;

        private static WordPressClient Client { get; }
        public static bool AlwaysUseAuthentication { get; set; }
        public static bool IsAuthenticated => !string.IsNullOrEmpty(Client.GetToken());

        static WPClientWrapper()
        {
            Client = new WordPressClient(new Uri(new Uri(WordpressEndpoint), "wp-json").ToString()) { AuthMethod = AuthMethod.JWT };
        }

        public static async Task<bool> AuthenticateAsync(string username, string password)
        {
            await Client.RequestJWToken(username, password);
            if (!await Client.IsValidJWToken())
                throw new WPException($"Cannot authenticate user '{username}' in WordPress because of invalid JWToken.");

            return IsAuthenticated;
        }

        public static async Task<IEnumerable<Category>> GetCategoriesAsync(bool embed = false)
        {
            return await Client.Categories.GetAll(embed, AlwaysUseAuthentication);
        }

        public static async Task<int> GetCategoryId(PostCategory category)
        {
            CategoriesQueryBuilder categoriesQueryBuilder = new() { Search = category.GetName() };
            var matches = await Client.Categories.Query(categoriesQueryBuilder, AlwaysUseAuthentication);
            
            return matches.Single(c => c.Name == category.GetName()).Id;
        }

        public static async Task<IEnumerable<Post>> GetPostsPageAsync(int categoryId, int page, bool embed = false, int perPage = PostsPerPage)
        {
            var queryBuilder = new PostsQueryBuilder() { Page = page, PerPage = perPage, Categories = new int[1] { categoryId } };
            string route = PostsPath + queryBuilder.BuildQueryURL();

            IEnumerable<Post> posts;
            try
            {
                posts = await GetCustomAsync<IEnumerable<Post>>(route, embed);
                posts = posts.Where(p => p.Status == Status.Publish);
            }
            catch (Exception) 
            {
                posts = Enumerable.Empty<Post>();
            }

            return posts;
        }

        public static async Task<IEnumerable<Post>> GetPostsAsync(int categoryId, bool embed = false)
        {
            int curPage = 1;
            List<Post> posts = new();
            IEnumerable<Post> nextPosts;

            do
            {
                nextPosts = await GetPostsPageAsync(categoryId, curPage++, embed);

                if (!nextPosts.Any())
                    break;

                posts.AddRange(nextPosts);
            } while (nextPosts.Count() % PostsPerPage == 0);

            return posts;
        }

        public static IEnumerable<Post> FilterPostsForSchool(this IEnumerable<Post> posts, SchoolUnique schoolUnique)
        {
            return posts.Where(p => schoolUnique.Equals(new SchoolUnique(p.GetTitle())));
        }

        public static async Task<TModelACF> GetAcfAsync<TModelACF>(int postId, bool embed = false) 
            where TModelACF : IModelAcf
        {
            string route = AcfPostsPath + $"/{postId}";
            var response = await GetCustomAsync<JObject>(route, embed);

            return response.GetValue("acf").ToObject<TModelACF>();
        }

        public static async Task<T> GetCustomAsync<T>(string route, bool embed = false)
            where T : class
        {
            return await Client.CustomRequest.Get<T>(route, embed, AlwaysUseAuthentication);
        }
    }
}
