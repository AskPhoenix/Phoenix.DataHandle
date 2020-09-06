using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class School
    {
        public School()
        {
            Classroom = new HashSet<Classroom>();
            Course = new HashSet<Course>();
            UserSchool = new HashSet<UserSchool>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string City { get; set; }
        public string AddressLine { get; set; }
        public string Info { get; set; }
        public string FacebookPageId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public virtual ICollection<Classroom> Classroom { get; set; }
        public virtual ICollection<Course> Course { get; set; }
        public virtual ICollection<UserSchool> UserSchool { get; set; }
    }
}
