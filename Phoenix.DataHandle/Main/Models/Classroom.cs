using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class Classroom
    {
        public Classroom()
        {
            Lectures = new HashSet<Lecture>();
            Schedules = new HashSet<Schedule>();
        }

        public int Id { get; set; }
        public int SchoolId { get; set; }
        public string Name { get; set; } = null!;
        public string NormalizedName { get; set; } = null!;
        public string? Comments { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? ObviatedAt { get; set; }

        public virtual School School { get; set; } = null!;
        public virtual ICollection<Lecture> Lectures { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
