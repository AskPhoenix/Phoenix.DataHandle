using System.Globalization;

namespace Phoenix.DataHandle.Utilities
{
    public static class StringExtensions
    {
        public static string UpperToTitleCase(this string str)
        {
            string tore = new string(str);

            bool allUpper = tore == tore.ToUpper();
            if (!allUpper)
                return tore;

            var textInfo = CultureInfo.InvariantCulture.TextInfo;

            tore = tore.ToLower();
            tore = textInfo.ToTitleCase(tore).Replace("σ ", "ς ").Replace("σ-", "ς-");

            if (tore.EndsWith('σ'))
            {
                char[] toreArr = tore.ToCharArray();
                toreArr[^1] = 'ς';
                tore = new string(toreArr);
            }

            return tore;
        }
    }
}
