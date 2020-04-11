using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Entities
{
    public interface ISchool
    {
        string name { get; set; }
        string city { get; set; }
        string addressLine { get; set; }
        //IPerson Owner { get; set; }
    }
}