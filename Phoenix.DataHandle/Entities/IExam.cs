using System;

namespace Phoenix.DataHandle.Entities
{
    public interface IExam
    {
        IBook Book { get; set; }
        string Chapter { get; set; }
        string Section { get; set; }
    }
}