namespace Phoenix.DataHandle.WordPress
{
    public enum PostCategory
    {
        Uncategorised = 1,
        SchoolInformation = 3,
        Course = 4,
        Schedule = 5,
        Personnel = 9,
        Client = 6
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
