using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IUser
    {
        IAspNetUser AspNetUser { get; }
        string FirstName { get; }
        string LastName { get; }
        string FullName { get; }
        bool IsSelfDetermined { get; }
        int DependenceOrder { get; }

        IEnumerable<IBotFeedback> BotFeedbacks { get; }
        IEnumerable<IBroadcast> Broadcasts { get; }
        IEnumerable<IGrade> Grades { get; }
        IEnumerable<IOneTimeCode> OneTimeCodes { get; }
        IEnumerable<IUserConnection> UserConnections { get; }
        
        IEnumerable<IUser> Children { get; }
        IEnumerable<ICourse> Courses { get; }
        IEnumerable<ILecture> Lectures { get; }
        IEnumerable<IUser> Parents { get; }
        IEnumerable<ISchool> Schools { get; }
    }
}