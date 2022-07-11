namespace Phoenix.DataHandle.Main.Models.Extensions
{
    public interface IConnectionEntity<TNormalizableEntity> : IModelEntity, INormalizableEntity<TNormalizableEntity>
        where TNormalizableEntity : class, IModelEntity
    {
        int TenantId { get; set; }
        string Channel { get; set; }
        string ChannelKey { get; set; }
        string ChannelDisplayName { get; set; }
        DateTime? ActivatedAt { get; set; }
        bool IsActive => ActivatedAt.HasValue;

        public static Func<string, string> NormFunc => s => s.ToUpperInvariant();
    }
}
