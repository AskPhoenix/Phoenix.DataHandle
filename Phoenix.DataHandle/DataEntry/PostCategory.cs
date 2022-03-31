namespace Phoenix.DataHandle.DataEntry
{
    public enum PostCategory
    {
        Uncategorised,          // 1
        DataEntry,              // 11
        SchoolInformation,      // 3
        Course,                 // 4
        Schedule,               // 5
        Personnel,              // 9
        Client                  // 6
    }

    public static class PostCategoryExtensions
    {
        public static string GetName(this PostCategory cat)
        {
            return cat switch
            {
                PostCategory.Uncategorised      => "Uncategorised",
                PostCategory.DataEntry          => "Data Entry",
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
