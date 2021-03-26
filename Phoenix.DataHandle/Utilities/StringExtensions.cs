using System.Globalization;

namespace Phoenix.DataHandle.Utilities
{
    public static class StringExtensions
    {
        //TODO: Locale
        public static string ToTitleCase(this string str, bool ignoreUpperCase = true)
        {
            if (str is null)
                return null;

            string tore = new string(str);
            var textInfo = CultureInfo.InvariantCulture.TextInfo;

            if (ignoreUpperCase)
                tore = tore.ToLowerInvariant();

            //Check if a non-invariant culture solves the problem
            tore = textInfo.ToTitleCase(tore).Replace("σ ", "ς ").Replace("σ-", "ς-").Replace("σ_", "ς_");

            if (tore.EndsWith('σ'))
            {
                char[] toreArr = tore.ToCharArray();
                toreArr[^1] = 'ς';
                tore = new string(toreArr);
            }

            return tore;
        }

        public static string Truncate(this string str, int maxLength)
        {
            return str?.Length <= maxLength ? str : str.Substring(0, maxLength);
        }
    }
}
