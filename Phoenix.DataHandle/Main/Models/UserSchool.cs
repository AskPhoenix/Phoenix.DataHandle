using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Phoenix.DataHandle.Main.Models
{
    public partial class UserSchool
    {
        public int AspNetUserId { get; set; }
        public int SchoolId { get; set; }
        public DateTimeOffset? EnrolledOn { get; set; }

        public virtual AspNetUsers AspNetUser { get; set; }
        public virtual School School { get; set; }
    }
}
