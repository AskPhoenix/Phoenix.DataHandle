namespace Phoenix.DataHandle.Main.Types
{
    public enum Tense
    {
        Never = 0,
        Anytime,
        Past,
        Future
    }

    public static class TenseExtensions
    {
        private static bool TensePredicate(Tense t, string str) =>
            t.ToString().Equals(str, StringComparison.OrdinalIgnoreCase);

        public static Tense ToTense(this string me)
        {
            return Enum.GetValues<Tense>().SingleOrDefault(t => TensePredicate(t, me));
        }

        public static bool TryToTense(this string me, out Tense tense)
        {
            tense = me.ToTense();

            return Enum.GetValues<Tense>().Any(t => TensePredicate(t, me));
        }
    }
}
