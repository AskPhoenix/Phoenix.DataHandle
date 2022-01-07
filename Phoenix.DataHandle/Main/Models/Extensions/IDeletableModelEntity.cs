using System;
using System.Collections.Generic;
using System.Text;

namespace Phoenix.DataHandle.Main.Models.Extensions
{
    internal interface IDeletableModelEntity : IModelEntity
    {
        bool IsDeleted { get; set; }
        DateTimeOffset? DeletedAt { get; set; }
    }
}
