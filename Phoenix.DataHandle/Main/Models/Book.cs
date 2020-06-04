using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class Book
    {
        public Book()
        {
            this.CourseBook = new HashSet<CourseBook>();
            this.Exercise = new HashSet<Exercise>();
            this.Material = new HashSet<Material>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CourseBook> CourseBook { get; set; }
        public virtual ICollection<Exercise> Exercise { get; set; }
        public virtual ICollection<Material> Material { get; set; }
    }
}
