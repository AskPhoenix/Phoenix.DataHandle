namespace Phoenix.DataHandle.WordPress
{
    public enum PostCategory
    {
        Uncategorised,
        SchoolInformation, 
        Course,
        Schedule,
        Personnel,
        Client
    }

    public static class PostCategoryExtensions
    {
        public static string GetName(this PostCategory cat)
        {
            return cat switch
            {
                PostCategory.Uncategorised      => "Uncategorised",
                PostCategory.SchoolInformation  => "School Information",
                PostCategory.Course             => "Course",
                PostCategory.Schedule           => "Schedule",
                PostCategory.Personnel          => "Personnel",
                PostCategory.Client             => "Client",
                _                               => string.Empty
            };
        }
    }
}
