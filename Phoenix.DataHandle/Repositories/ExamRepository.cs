using System;
using System.Linq;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Repositories
{
    public class ExamRepository : Repository<Exam>
    {
        public ExamRepository(PhoenixContext dbContext) : base(dbContext) { }

        public override Exam Create(Exam tModel)
        {
            tModel.CreatedAt = DateTimeOffset.Now;
            
            return base.Create(tModel);
        }

        public override Exam Update(Exam tModel)
        {
            tModel.UpdatedAt = DateTimeOffset.Now;

            return base.Update(tModel);
        }

        public IQueryable<StudentExam> FindStudentExams(int id)
        {
            this.Include(a => a.StudentExam);
            return this.Find().Where(a => a.Id == id).SelectMany(a => a.StudentExam);
        }
    }
}
