namespace Phoenix.DataHandle.Main
{
    public enum LectureCreatedBy
    {
        Unknown = -1,
        Automatic = 1,
        Manual = 2,
    }

    public static class LectureGeneratedByExtensions
    {
        public static string ToGreekString(this LectureCreatedBy ls)
        {
            return ls switch
            {
                LectureCreatedBy.Unknown => "Άγνωστη",
                LectureCreatedBy.Automatic => "Αυτόματα",
                LectureCreatedBy.Manual => "Χειροκίνητα",
                _ => ls.ToString(),
            };
        }
    }
}