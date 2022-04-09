using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class School
    {
        public School()
        {
            Broadcasts = new HashSet<Broadcast>();
            Classrooms = new HashSet<Classroom>();
            Courses = new HashSet<Course>();
            SchoolLogins = new HashSet<SchoolLogin>();
            Users = new HashSet<AspNetUser>();
        }

        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string City { get; set; } = null!;
        public string AddressLine { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? ObviatedAt { get; set; }

        public virtual SchoolInfo SchoolInfo { get; set; } = null!;
        public virtual ICollection<Broadcast> Broadcasts { get; set; }
        public virtual ICollection<Classroom> Classrooms { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<SchoolLogin> SchoolLogins { get; set; }

        public virtual ICollection<AspNetUser> Users { get; set; }
    }
}
