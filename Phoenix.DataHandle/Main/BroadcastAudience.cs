namespace Phoenix.DataHandle.Main
{
    public enum BroadcastAudience
    {
        None = 0,
        Students,
        Parents,
        StudentsParents,
        Staff,
        All
    }

    public static class BroadcastAudienceExtensions
    {
        public static string ToFriendlyString(this BroadcastAudience me)
        {
            return me switch
            {
                BroadcastAudience.Students => "Μαθητές",
                BroadcastAudience.Parents => "Γονείς",
                BroadcastAudience.StudentsParents => "Μαθητές & Γονείς",
                BroadcastAudience.Staff => "Διδακτικό προσωπικό",
                BroadcastAudience.All => "Όλοι",
                _ => string.Empty
            };
        }
    }
}
