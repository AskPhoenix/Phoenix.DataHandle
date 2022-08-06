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
        public static Tense ToTense(this string me)
        {
            return Enum.GetValues<Tense>()
                    .SingleOrDefault(t => t.ToString().Equals(me, StringComparison.OrdinalIgnoreCase));
        }

        public static bool TryToTense(this string me, out Tense tense)
        {
            tense = me.ToTense();

            return Enum.GetValues<Tense>()
                .Any(t => t.ToString().Equals(me, StringComparison.OrdinalIgnoreCase));
        }
    }
}
