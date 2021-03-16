using System.Linq;
using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Repositories
{
    public class ExamRepository : Repository<Exam>
    {
        public ExamRepository(PhoenixContext dbContext) : base(dbContext) { }

        public IQueryable<StudentExam> FindStudentExams(int id)
        {
            this.Include(a => a.StudentExam);
            return this.Find().Where(a => a.Id == id).SelectMany(a => a.StudentExam);
        }

        public IQueryable<Exam> FindForStudent(int studentId)
        {
            return this.dbContext.Set<StudentExam>().
                Include(se => se.Exam).
                Where(se => se.StudentId == studentId).
                Select(se => se.Exam);
        }

        public IQueryable<Exam> FindForStudent(int studentId, int lectureId)
        {
            return this.dbContext.Set<StudentExam>().
                Include(se => se.Exam).
                Where(se => se.StudentId == studentId && se.Exam.LectureId == lectureId).
                Select(se => se.Exam);
        }
    }
}
