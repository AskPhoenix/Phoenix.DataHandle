using System;
using System.Collections.Generic;
using System.Linq;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Relationships;

namespace Phoenix.Api.Models.Api
{
    public class StudentExamApi : IStudentExam
    {
        public decimal? Grade { get; set; }

        public UserApi Student { get; set; }
        IUser IStudentExam.Student => this.Student;

        public ExamApi Exam { get; set; }
        IExam IStudentExam.Exam => this.Exam;
    }
}
