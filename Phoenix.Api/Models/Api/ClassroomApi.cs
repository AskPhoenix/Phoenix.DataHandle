using System;
using System.Collections.Generic;
using System.Linq;
using Phoenix.DataHandle.Main.Entities;

namespace Phoenix.Api.Models.Api
{
    public class ClassroomApi : IClassroom, IModelApi
    {
        public int id { get; set; }
        public ISchool School { get; }
        public string Name { get; set; }
        public string Info { get; set; }
        public IEnumerable<IExam> Exams { get; }
        public IEnumerable<ILecture> Lectures { get; }
    }
}
