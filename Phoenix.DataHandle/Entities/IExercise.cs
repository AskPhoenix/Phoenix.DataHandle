using System;

namespace Phoenix.DataHandle.Entities
{
    public interface IExercise
    {
        IBook Book { get; set; }
        string page { get; set; }
        string number { get; set; }

    }
}