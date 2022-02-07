using System.Globalization;

namespace Phoenix.DataHandle.Utilities
{
    public static class StringExtensions
    {
        public static string ToTitleCase(this string str)
        {
            var textInfo = CultureInfo.InvariantCulture.TextInfo;

            return textInfo.ToTitleCase(new(str));
        }

        public static string Truncate(this string str, int maxLength)
        {
            return str.Length <= maxLength ? str : str[..maxLength];
        }
    }
}
