using System;

namespace Phoenix.DataHandle.Main.Models.Extensions
{
    public interface IModelEntity<TKey>
    {
        TKey Id { get; }
        DateTimeOffset CreatedAt { get; }
        DateTimeOffset? UpdatedAt { get; }
    }

    public interface IModelEntity : IModelEntity<int> { }
}
