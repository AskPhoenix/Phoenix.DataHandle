﻿namespace Phoenix.DataHandle.Main
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
            switch (ls)
            {
                case LectureCreatedBy.Unknown:
                    return "Άγνωστη";
                case LectureCreatedBy.Automatic:
                    return "Αυτόματα";
                case LectureCreatedBy.Manual:
                    return "Χειροκίνητα";
            }

            return ls.ToString();
        }
    }
}