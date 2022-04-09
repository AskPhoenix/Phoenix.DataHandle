using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class Book
    {
        public Book()
        {
            Exercises = new HashSet<Exercise>();
            Materials = new HashSet<Material>();
            Courses = new HashSet<Course>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string NormalizedName { get; set; } = null!;
        public string? Publisher { get; set; }
        public string? Comments { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Exercise> Exercises { get; set; }
        public virtual ICollection<Material> Materials { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
