using System;

namespace Phoenix.DataHandle.Main.Models.Extensions
{
    public interface IObviableModelEntity : IModelEntity
    {
        bool IsObviated { get => ObviatedAt.HasValue; }
        DateTimeOffset? ObviatedAt { get; set; }
    }
}
