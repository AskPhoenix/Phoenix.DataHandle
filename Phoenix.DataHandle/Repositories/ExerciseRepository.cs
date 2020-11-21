using System;
using System.Linq;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Repositories
{
    public class ExerciseRepository : Repository<Exercise>
    {
        public ExerciseRepository(PhoenixContext dbContext) : base(dbContext) { }

        public override Exercise Create(Exercise tModel)
        {
            if (tModel == null)
                throw new ArgumentNullException(nameof(tModel));

            tModel.CreatedAt = DateTimeOffset.Now;
            
            return base.Create(tModel);
        }

        public override Exercise Update(Exercise tModel)
        {
            if (tModel == null)
                throw new ArgumentNullException(nameof(tModel));

            tModel.UpdatedAt = DateTimeOffset.Now;

            return base.Update(tModel);
        }

        public IQueryable<StudentExercise> FindStudentExercises(int id)
        {
            this.Include(a => a.StudentExercise);
            return this.Find().Where(a => a.Id == id).SelectMany(a => a.StudentExercise);
        }
    }
}
