using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Main.Models;

namespace Phoenix.DataHandle.Repositories
{
    public class ExamRepository : Repository<Exam>
    {
        public ExamRepository(PhoenixContext dbContext) : base(dbContext)
        {
        }

        public override Exam create(Exam tModel)
        {
            tModel.CreatedAt = DateTimeOffset.Now;
            
            return base.create(tModel);
        }

        public override Exam update(Exam tModel)
        {
            tModel.UpdatedAt = DateTimeOffset.Now;

            return base.update(tModel);
        }

        public IQueryable<StudentExam> FindStudentExams(int id)
        {
            this.include(a => a.StudentExam);
            return this.find().Where(a => a.Id == id).SelectMany(a => a.StudentExam);
        }

    }
}
