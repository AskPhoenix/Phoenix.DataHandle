using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Models
{
    public partial class Classroom
    {
        public Classroom()
        {
            Lecture = new HashSet<Lecture>();
        }

        public int Id { get; set; }
        public int SchoolId { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual School School { get; set; }
        public virtual ICollection<Lecture> Lecture { get; set; }
    }
}
