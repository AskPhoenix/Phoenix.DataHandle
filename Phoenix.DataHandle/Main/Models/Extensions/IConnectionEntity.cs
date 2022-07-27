using Phoenix.DataHandle.Base.Entities;

namespace Phoenix.DataHandle.Main.Models.Extensions
{
    public interface IConnectionEntity : IModelEntity, IConnectionBase
    {
        int TenantId { get; set; }
        new string Channel { get; set; }
        new string ChannelKey { get; set; }
        new string ChannelDisplayName { get; set; }
        new DateTime? ActivatedAt { get; set; }
    }
}
