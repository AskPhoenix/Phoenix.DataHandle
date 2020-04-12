using System;

namespace Phoenix.DataHandle.Entities
{
    public interface IUser
    {
        string surname { get; set; }
        string forename { get; set; }
    }
}