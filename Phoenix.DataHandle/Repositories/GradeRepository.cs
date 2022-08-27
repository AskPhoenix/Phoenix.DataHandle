using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Repositories
{
    public sealed class GradeRepository : Repository<Grade>
    {
        public GradeRepository(PhoenixContext phoenixContext)
            : base(phoenixContext)
        {
        }

        #region Search

        public IQueryable<Grade> Search(int? studentId = null, int? courseId = null,
            int? examId = null, int? exerciseId = null)
        {
            var grades = Find();

            if (studentId.HasValue)
                grades = grades.Where(g => g.StudentId == studentId);
            if (courseId.HasValue)
                grades = grades.Where(g => g.CourseId == courseId);
            if (examId.HasValue)
                grades = grades.Where(g => g.ExamId == examId);
            if (exerciseId.HasValue)
                grades = grades.Where(g => g.ExerciseId == exerciseId);

            return grades;
        }

        #endregion
    }
}
