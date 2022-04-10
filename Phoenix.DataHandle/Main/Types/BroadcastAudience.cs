using Phoenix.Language.Types;

namespace Phoenix.DataHandle.Main.Types
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
        Everyone
    }

    public static class BroadcastAudienceExtensions
    {
        public static string ToFriendlyString(this BroadcastAudience me)
        {
            return me switch
            {
                BroadcastAudience.Students          => BroadcastAudienceResources.Students,
                BroadcastAudience.Parents           => BroadcastAudienceResources.Parents,
                BroadcastAudience.Staff             => BroadcastAudienceResources.Staff,
                BroadcastAudience.StudentsParents   => BroadcastAudienceResources.StudentsParents,
                BroadcastAudience.StudentsStaff     => BroadcastAudienceResources.StudentsStaff,
                BroadcastAudience.ParentsStaff      => BroadcastAudienceResources.ParentsStaff,
                BroadcastAudience.Everyone          => BroadcastAudienceResources.Everyone,
                _                                   => string.Empty
            };
        }
    }
}
