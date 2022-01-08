using System;
using System.Collections.Generic;
using System.Text;

namespace Phoenix.DataHandle.Main.Models.Extensions
{
    internal interface IObviableModelEntity : IModelEntity
    {
        bool IsObviated { get; set; }
        DateTimeOffset? ObviatedAt { get; set; }
    }
}
