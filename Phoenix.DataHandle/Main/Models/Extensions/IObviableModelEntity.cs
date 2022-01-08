using System;

namespace Phoenix.DataHandle.Main.Models.Extensions
{
    public interface IObviableModelEntity : IModelEntity
    {
        bool IsObviated { get; set; }
        DateTimeOffset? ObviatedAt { get; set; }
    }
}
