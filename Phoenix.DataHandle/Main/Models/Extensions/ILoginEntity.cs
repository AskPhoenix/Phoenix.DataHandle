using Phoenix.DataHandle.Main.Entities;
using System;

namespace Phoenix.DataHandle.Main.Models.Extensions
{
    public interface ILoginEntity : IModelEntity
    {
        int TenantId { get; set; }
        int ChannelId { get; set; }
        IChannel Channel { get; }
        string ProviderKey { get; set; }
        bool IsActive => ActivatedAt.HasValue;
        DateTime? ActivatedAt { get; set; }
    }
}
