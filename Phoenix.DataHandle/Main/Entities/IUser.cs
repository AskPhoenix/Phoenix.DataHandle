using System.Collections.Generic;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IUser
    {
        string UserName { get; }
        string? Email { get; }
        string PhoneNumber { get; }
        string PhoneCountryCode { get; }
        int DependenceOrder { get; } // Dependance order separates non-self-dependent users. The phone owner takes the order 0.

        IUserInfo UserInfo { get; }
        IEnumerable<IBotFeedback> BotFeedbacks { get; }
        IEnumerable<IBroadcast> Broadcasts { get; }
        IEnumerable<IGrade> Grades { get; }
        IEnumerable<IOneTimeCode> OneTimeCodes { get; }
        IEnumerable<IUserLogin> UserLogins { get; }

        IEnumerable<IUser> Children { get; }
        IEnumerable<ICourse> Courses { get; }
        IEnumerable<ILecture> Lectures { get; }
        IEnumerable<IUser> Parents { get; }
        IEnumerable<IRole> Roles { get; }
        IEnumerable<ISchool> Schools { get; }
    }
}
