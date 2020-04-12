using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Models
{
    public partial class Course
    {
        public Course()
        {
            Lecture = new HashSet<Lecture>();
        }

        public int id { get; set; }
        public int schoolId { get; set; }
        public string name { get; set; }
        public string level { get; set; }
        public string group { get; set; }
        public string info { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set; }

        public virtual School school { get; set; }
        public virtual ICollection<Lecture> Lecture { get; set; }
    }
}
