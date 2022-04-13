using System;

namespace Phoenix.DataHandle.Main.Models.Extensions
{
    public interface IModelEntity
    {
        int Id { get; }
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
    }
}
