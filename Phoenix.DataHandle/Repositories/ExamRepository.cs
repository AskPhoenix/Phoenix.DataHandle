using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Phoenix.DataHandle.Main;
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

        public decimal? FindGrade(int studentId, int examId)
        {
            return this.dbContext.Set<StudentExam>().
                Single(se => se.StudentId == studentId && se.ExamId == examId).
                Grade;
        }

        public IQueryable<Exam> FindForStudent(int studentId, Tense tense = Tense.Anytime)
        {
            var studentExams = this.dbContext.Set<StudentExam>().
                Include(se => se.Exam).
                ThenInclude(e => e.Material).
                ThenInclude(m => m.Book).
                Include(se => se.Exam.Lecture).
                Where(se => se.StudentId == studentId);

            if (tense == Tense.Past)
                studentExams = studentExams.Where(se => se.Exam.Lecture.StartDateTime < DateTimeOffset.UtcNow);
            else if (tense == Tense.Future)
                studentExams = studentExams.Where(se => se.Exam.Lecture.StartDateTime >= DateTimeOffset.UtcNow);

            return studentExams.Select(se => se.Exam);
        }

        public IQueryable<Exam> FindForLecture(int lectureId)
        {
            return this.dbContext.Set<Exam>().
                Include(e => e.Material).
                ThenInclude(m => m.Book).
                Where(e => e.LectureId == lectureId);
        }
    }
}
