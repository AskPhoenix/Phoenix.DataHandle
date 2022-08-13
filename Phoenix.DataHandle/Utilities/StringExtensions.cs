using System.Globalization;
using System.Text;

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

        public static string ToUnaccented(this string me)
        {
            string fullCanonicalDecompositionNormalized = me.Normalize(NormalizationForm.FormD);
            var unaccented = fullCanonicalDecompositionNormalized
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark);

            return new string(unaccented.ToArray());
        }

        public static string RemoveEmojis(this string me, bool postTrim = true)
        {
            string newMe = new(me.Where(c => !char.IsSurrogate(c) && !char.IsSymbol(c)).ToArray());

            if (postTrim)
                newMe = newMe.Trim();

            return newMe;
        }
    }
}
