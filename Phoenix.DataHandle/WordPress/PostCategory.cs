namespace Phoenix.DataHandle.WordPress
{
    public enum PostCategory
    {
        Uncategorised,
        School, 
        Course,
        Schedule,
        SchoolUser
    }

    public static class PostCategoryExtensions
    {
        public static string GetName(this PostCategory cat)
        {
            return cat switch
            {
                PostCategory.Uncategorised => "Uncategorised",
                PostCategory.School => "School",
                PostCategory.Course => "Course",
                PostCategory.Schedule => "Schedule",
                PostCategory.SchoolUser => "School User",
                _ => string.Empty,
            };
        }
    }
}
