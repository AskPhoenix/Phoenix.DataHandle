using Phoenix.DataHandle.Main.Entities;
using System;

namespace Phoenix.DataHandle.Main.Relationships
{
    public interface IUserSchool
    {
        DateTimeOffset? EnrolledOn { get; set; }
        
        IAspNetUsers AspNetUser { get; }
        ISchool School { get; }
    }
}
