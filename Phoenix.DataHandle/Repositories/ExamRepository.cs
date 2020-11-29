using System.Linq;
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
    }
}
