using Phoenix.DataHandle.Base.Entities;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IUser : IUserBase
    {
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