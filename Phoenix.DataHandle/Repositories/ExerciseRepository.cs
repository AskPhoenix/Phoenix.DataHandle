using System.Linq;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Repositories
{
    public class ExerciseRepository : Repository<Exercise>
    {
        public ExerciseRepository(PhoenixContext dbContext) : base(dbContext) { }

        public IQueryable<StudentExercise> FindStudentExercises(int id)
        {
            this.Include(a => a.StudentExercise);
            return this.Find().Where(a => a.Id == id).SelectMany(a => a.StudentExercise);
        }
    }
}
