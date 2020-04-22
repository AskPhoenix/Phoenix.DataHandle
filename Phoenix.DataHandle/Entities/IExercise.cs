using System;

namespace Phoenix.DataHandle.Entities
{
    public interface IExercise
    {
        IBook Book { get; set; }
        string Page { get; set; }
        string Number { get; set; }

    }
}