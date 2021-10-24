namespace Phoenix.DataHandle.Main
{
    public enum BroadcastAudience
    {
        None = 0,
        Students,
        Parents,
        Staff,
        StudentsParents,
        StudentsStaff,
        ParentsStaff,
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
                BroadcastAudience.Staff => "Διδακτικό προσωπικό",
                BroadcastAudience.StudentsParents => "Μαθητές & Γονείς",
                BroadcastAudience.StudentsStaff => "Μαθητές & Προσωπικό",
                BroadcastAudience.ParentsStaff => "Γονείς & Προσωπικό",
                BroadcastAudience.All => "Όλοι",
                _ => string.Empty
            };
        }
    }
}
