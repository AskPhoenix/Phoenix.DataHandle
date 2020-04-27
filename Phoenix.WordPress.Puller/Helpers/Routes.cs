namespace Phoenix.WordPress.Puller.Helpers
{
    internal static class Routes
    {
        public const string Users = "wp/v2/users";
        public const string Posts = "wp/v2/posts";

        public static string ForCategory(int categoryId) => Posts + $"?categories={categoryId}";
    }
}
