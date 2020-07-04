using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Repositories
{
    public class ExerciseRepository : Repository<Exercise>
    {
        public ExerciseRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override Exercise create(Exercise tModel)
        {
            tModel.CreatedAt = DateTime.Now;
            
            return base.create(tModel);
        }

        public override Exercise update(Exercise tModel)
        {
            tModel.UpdatedAt = DateTime.Now;

            return base.update(tModel);
        }

        public IQueryable<StudentExercise> FindStudentExercises(int id)
        {
            this.include(a => a.StudentExercise);
            return this.find().Where(a => a.Id == id).SelectMany(a => a.StudentExercise);
        }

    }
}
