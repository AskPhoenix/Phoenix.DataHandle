using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Entities
{
    public interface IParent
    {
        IEnumerable<IStudent> Students { get; set; }
    }
}