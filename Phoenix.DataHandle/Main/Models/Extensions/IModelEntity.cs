using System;

namespace Phoenix.DataHandle.Main.Models.Extensions
{
    public interface IModelEntity
    {
        int Id { get; }
        DateTimeOffset CreatedAt { get; set; }
        DateTimeOffset? UpdatedAt { get; set; }
    }
}
