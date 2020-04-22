using System;

namespace Phoenix.DataHandle.Entities
{
    public interface IUser
    {
        string Surname { get; set; }
        string Forename { get; set; }
    }
}