using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Models
{
    public partial class Lecture
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int ClassroomId { get; set; }
        public string Name { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int Status { get; set; }
        public string Info { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Classroom Classroom { get; set; }
        public virtual Course Course { get; set; }
    }
}
