namespace Phoenix.DataHandle.Main
{
    public enum LectureStatus
    {
        Unknown = -1,
        Scheduled = 1,
        Cancelled = 2,
    }

    public static class LectureStatusExtensions
    {
        public static string ToGreekString(this LectureStatus ls)
        {
            return ls switch
            {
                LectureStatus.Unknown => "Άγνωστη",
                LectureStatus.Scheduled => "Κανονικά",
                LectureStatus.Cancelled => "Ακυρώθηκε",
                _ => ls.ToString(),
            };
        }
    }
}