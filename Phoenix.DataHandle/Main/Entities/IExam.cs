﻿using Phoenix.DataHandle.Base;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IExam : IExamBase
    {
        ILecture Lecture { get; }
        
        IEnumerable<IGrade> Grades { get; }
        IEnumerable<IMaterial> Materials { get; }
    }
}