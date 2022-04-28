namespace Phoenix.DataHandle.Main.Models.Extensions
{
    public interface IConnectionEntity : IModelEntity
    {
        int TenantId { get; set; }
        string Channel { get; set; }
        string ChannelKey { get; set; }
        string ChannelDisplayName { get; set; }
        DateTime? ActivatedAt { get; set; }
        bool IsActive => ActivatedAt.HasValue;
    }
}
