using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Phoenix.DataHandle.Main.Models
{
    public partial class Material
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public int? BookId { get; set; }
        public string Chapter { get; set; }
        public string Section { get; set; }
        public string Comments { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public virtual Book Book { get; set; }
        public virtual Exam Exam { get; set; }
    }
}
