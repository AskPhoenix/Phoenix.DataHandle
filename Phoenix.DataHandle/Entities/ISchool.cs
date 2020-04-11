using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Entities
{
    public interface ISchool
    {
        string Name { get; set; }
        string City { get; set; }
        string AddressLine { get; set; }
        IPerson Owner { get; set; }
    }
}