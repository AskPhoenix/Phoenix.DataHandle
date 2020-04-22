using System;

namespace Phoenix.DataHandle.Entities
{
    public interface IUser
    {
        string FirstName { get; set; }
        string LastName { get; set; }
    }
}