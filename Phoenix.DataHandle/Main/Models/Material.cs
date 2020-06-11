using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class Material
    {
        public int Id { get; set; }
        public int? ExamId { get; set; }
        public int? BookId { get; set; }
        public string Chapter { get; set; }
        public string Section { get; set; }
        public string Comments { get; set; }

        public virtual Book Book { get; set; }
        public virtual Exam Exam { get; set; }
    }
}
