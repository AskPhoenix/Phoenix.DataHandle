using System;
using System.Linq;

namespace Phoenix.DataHandle.Main
{
    public enum Tense
    {
        Anytime,
        Past,
        Future
    }

    public static class TenseExtensions
    {
        public static Tense ToTense(this string me)
        {
            return Enum.GetValues(typeof(Tense)).Cast<Tense>().
                    SingleOrDefault(t => string.Equals(t.ToString(), me, StringComparison.OrdinalIgnoreCase));
        }
    }
}
