using System;
using System.Collections.Generic;
using System.Linq;
using Phoenix.DataHandle.Main.Entities;

namespace Phoenix.Api.Models.Api
{
    public class MaterialApi : IMaterial, IModelApi
    {
        public int id { get; set; }
        public string Chapter { get; set; }
        public string Section { get; set; }
        public string Comments { get; set; }

        public BookApi Book { get; set; }
        IBook IMaterial.Book => this.Book;

        public ExamApi Exam { get; set; }
        IExam IMaterial.Exam => this.Exam;
    }
}
