using System;

namespace Phoenix.DataHandle.Main.Models.Extensions
{
    public interface IModelEntity<out TKey>
    {
        TKey Id { get; }
    }

    public interface IModelEntity : IModelEntity<int> { }
}
