using System;
using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Models
{
    public partial class Grade
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int? CourseId { get; set; }
        public int? ExamId { get; set; }
        public int? ExerciseId { get; set; }
        public decimal Score { get; set; }
        public string? Topic { get; set; }
        public string? Justification { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Course? Course { get; set; }
        public virtual Exam? Exam { get; set; }
        public virtual Exercise? Exercise { get; set; }
        public virtual UserInfo Student { get; set; } = null!;
    }
}
