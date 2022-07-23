using Phoenix.DataHandle.Base.Entities;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IBotFeedback : IBotFeedbackBase
    {
        IUser Author { get; }
    }
}
