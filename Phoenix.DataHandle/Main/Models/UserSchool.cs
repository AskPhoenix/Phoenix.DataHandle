using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class UserSchool
    {
        public int AspNetUserId { get; set; }
        public int SchoolId { get; set; }
        public DateTime? EnrolledOn { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual AspNetUsers AspNetUser { get; set; }
        public virtual School School { get; set; }
    }
}
