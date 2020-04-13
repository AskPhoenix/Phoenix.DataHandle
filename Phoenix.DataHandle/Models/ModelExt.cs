using System;
using System.Collections.Generic;
using Phoenix.DataHandle.Entities;

namespace Phoenix.DataHandle.Models
{
    public partial class School : ISchool, IModelDb { }

    public partial class User : IUser, IModelDb { }

    public partial class Course : ICourse, IModelDb
    {
        ISchool ICourse.School => this.school;
        IEnumerable<ILecture> ICourse.Lectures => this.Lecture;
    }

    public partial class Lecture : ILecture, IModelDb
    {
        ICourse ILecture.Course => this.course;
    }
}
