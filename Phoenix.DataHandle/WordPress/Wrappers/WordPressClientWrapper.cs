using Newtonsoft.Json.Linq;
using Phoenix.DataHandle.WordPress.Models.ACF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordPressPCL;
using WordPressPCL.Models;
using WordPressPCL.Utility;

namespace Phoenix.DataHandle.WordPress.Wrappers
{
    public static class WordPressClientWrapper
    {
        private const string WordpressEndpoint = "https://www.askphoenix.gr/";
        private const string PostsPath = "wp/v2/posts";
        private const string AcfPostsPath = "acf/v3/posts";

        private static WordPressClient Client { get; set; }
        public static bool AlwaysUseAuthentication { get; set; } = false;

        static WordPressClientWrapper()
        {
            Client = new WordPressClient(new Uri(new Uri(WordpressEndpoint), "wp-json").ToString()) { AuthMethod = AuthMethod.JWT };
        }

        public static async Task<bool> AuthenticateAsync(string username, string password)
        {
            await Client.RequestJWToken(username, password);
            if (!await Client.IsValidJWToken())
            {
                string errorMsg = $"Cannot authenticate user '{username}' in WordPress because of invalid JWToken.";
                throw new Exception(errorMsg);
            }

            return IsAuthenticated;
        }

        public static bool IsAuthenticated { get => !string.IsNullOrEmpty(Client.GetToken()); }

        public static async Task<IEnumerable<Category>> GetCategoriesAsync(bool embed = false) 
            => await Client.Categories.GetAll(embed, AlwaysUseAuthentication);

        public static async Task<IEnumerable<Post>> GetPaginatedPostsByCategoryAsync(int categoryId, int page, int perPage = 10, bool embed = false)
        {
            var queryBuilder = new PostsQueryBuilder() { Page = page, PerPage = perPage, Categories = new int[1] { categoryId } };
            string route = PostsPath + queryBuilder.BuildQueryURL();

            IEnumerable<Post> posts;
            try
            {
                posts = await Client.CustomRequest.Get<IEnumerable<Post>>(route, embed, AlwaysUseAuthentication);
            }
            catch (Exception) 
            {
                posts = Enumerable.Empty<Post>();
            }

            return posts;
        }

        public static async Task<IAcfModel<CtxT>> GetAcfAsync<CtxT>(int postId, bool embed = false)
        {
            string route = AcfPostsPath + $"/{postId}";
            var response = await GetCustomAsync<JObject>(route, embed);

            return response.GetValue("acf").ToObject<IAcfModel<CtxT>>();
        }

        public static async Task<T> GetCustomAsync<T>(string route, bool embed = false)
            where T : class
        {
            return await Client.CustomRequest.Get<T>(route, embed, AlwaysUseAuthentication);
        }
    }
}
