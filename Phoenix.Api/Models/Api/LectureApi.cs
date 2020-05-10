using System;
using System.Collections.Generic;
using System.Linq;
using Phoenix.DataHandle.Main.Entities;
using Phoenix.DataHandle.Main.Relationships;

namespace Phoenix.Api.Models.Api
{
    public class LectureApi : ILecture, IModelApi
    {
        public int id { get; set; }
        public ICourse Course { get; set; }
        public IClassroom Classroom { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int Status { get; set; }
        public string Info { get; set; }
        public IEnumerable<IAttendance> Attendances { get; }
        public IEnumerable<IHomework> Homeworks { get; }
    }
}
