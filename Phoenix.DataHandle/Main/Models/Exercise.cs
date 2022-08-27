namespace Phoenix.DataHandle.Main.Models
{
    public partial class Exercise
    {
        public Exercise()
        {
            Grades = new HashSet<Grade>();
        }

        public int Id { get; set; }
        public int LectureId { get; set; }
        public string Name { get; set; } = null!;
        public int? BookId { get; set; }
        public string? Page { get; set; }
        public string? Comments { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Book? Book { get; set; }
        public virtual Lecture Lecture { get; set; } = null!;
        public virtual ICollection<Grade> Grades { get; set; }
    }
}
