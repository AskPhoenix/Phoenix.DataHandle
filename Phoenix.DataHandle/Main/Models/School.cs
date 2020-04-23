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
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string City { get; set; }
        public string AddressLine { get; set; }
        public string Info { get; set; }
        public string Endpoint { get; set; }
        public string FacebookPageId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Classroom> Classroom { get; set; }
        public virtual ICollection<Course> Course { get; set; }
    }
}
