using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class UserSchool
    {
        public int AspNetUserId { get; set; }
        public int SchoolId { get; set; }
        public DateTimeOffset? EnrolledOn { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public virtual AspNetUsers AspNetUser { get; set; }
        public virtual School School { get; set; }
    }
}
