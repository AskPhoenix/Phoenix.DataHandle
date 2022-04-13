using Phoenix.DataHandle.Main.Entities;
using System;

namespace Phoenix.DataHandle.Main.Models.Extensions
{
    public interface ILoginEntity : IModelEntity
    {
        IChannel Channel { get; }
        string ProviderKey { get; }
        bool IsActive { get; }
        DateTime? ActivatedAt { get; }
    }
}
