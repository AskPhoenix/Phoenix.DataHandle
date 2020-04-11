using System;

namespace Phoenix.DataHandle.Entities
{
    public interface IStudent_Exam
    {
        IStudent Student { get; set; }
        IExam Exam { get; set; }
        float? Grade { get; set; }
    }
}