using System.Linq;
using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Repositories
{
    public class ExerciseRepository : Repository<Exercise>
    {
        public ExerciseRepository(PhoenixContext dbContext) : base(dbContext) { }

        public IQueryable<StudentExercise> FindStudentExercises(int exerciseId)
        {
            this.Include(a => a.StudentExercise);
            return this.Find().
                Where(a => a.Id == exerciseId).
                SelectMany(a => a.StudentExercise);
        }

        public IQueryable<Exercise> FindForStudent(int studentId)
        {
            return this.dbContext.Set<StudentExercise>().
                Include(se => se.Exercise).
                Where(se => se.StudentId == studentId).
                Select(se => se.Exercise);
        }

        public IQueryable<Exercise> FindForStudent(int studentId, int lectureId)
        {
            return this.dbContext.Set<StudentExercise>().
                Include(se => se.Exercise).
                Where(se => se.StudentId == studentId && se.Exercise.LectureId == lectureId).
                Select(se => se.Exercise);
        }
    }
}
