using System;

namespace Phoenix.DataHandle.Entities
{
    public interface IPerson
    {
        string Surname { get; set; }
        string Forename { get; set; }
        string Email { get; set; }
        Role Role { get; set; }
    }
}