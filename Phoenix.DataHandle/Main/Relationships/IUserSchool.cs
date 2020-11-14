using Phoenix.DataHandle.Main.Entities;
using System;

namespace Phoenix.DataHandle.Main.Relationships
{
    public interface IUserSchool
    {
        short Code { get; set; }
        DateTimeOffset? EnrolledOn { get; set; }
        DateTimeOffset CreatedAt { get; set; }
        DateTimeOffset? UpdatedAt { get; set; }

        IAspNetUsers AspNetUser { get; }
        ISchool School { get; }
    }
}
