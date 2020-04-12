using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Models
{
    public partial class Lecture
    {
        public int id { get; set; }
        public int courseId { get; set; }
        public int classroomId { get; set; }
        public string name { get; set; }
        public DateTime startDateTime { get; set; }
        public DateTime endDateTime { get; set; }
        public int status { get; set; }
        public string info { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set; }

        public virtual Classroom classroom { get; set; }
        public virtual Course course { get; set; }
    }
}
