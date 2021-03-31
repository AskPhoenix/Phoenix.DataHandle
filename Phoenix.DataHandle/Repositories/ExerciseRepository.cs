using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Main;
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

        public decimal? FindGrade(int studentId, int exerciseId)
        {
            return this.dbContext.Set<StudentExercise>().
                Single(se => se.StudentId == studentId && se.ExerciseId == exerciseId).
                Grade;
        }

        public IQueryable<Exercise> FindForStudent(int studentId, Tense tense = Tense.Anytime)
        {
            var studentExercises = this.dbContext.Set<StudentExercise>().
                Include(se => se.Exercise).
                ThenInclude(e => e.Book).
                Include(se => se.Exercise.Lecture).
                Where(se => se.StudentId == studentId);

            if (tense == Tense.Past)
                studentExercises = studentExercises.Where(se => se.Exercise.Lecture.StartDateTime < DateTimeOffset.UtcNow);
            else if (tense == Tense.Future)
                studentExercises = studentExercises.Where(se => se.Exercise.Lecture.StartDateTime >= DateTimeOffset.UtcNow);

            return studentExercises.Select(se => se.Exercise);
        }

        public IQueryable<Exercise> FindForLecture(int lectureId)
        {
            return this.dbContext.Set<Exercise>().
                Include(m => m.Book).
                Where(e => e.LectureId == lectureId);
        }
    }
}
