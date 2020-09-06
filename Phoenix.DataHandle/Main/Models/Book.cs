using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class Book
    {
        public Book()
        {
            CourseBook = new HashSet<CourseBook>();
            Exercise = new HashSet<Exercise>();
            Material = new HashSet<Material>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Info { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public virtual ICollection<CourseBook> CourseBook { get; set; }
        public virtual ICollection<Exercise> Exercise { get; set; }
        public virtual ICollection<Material> Material { get; set; }
    }
}
