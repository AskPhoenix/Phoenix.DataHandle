using Phoenix.DataHandle.Base;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IBotFeedback : IBotFeedbackBase
    {
        IUser Author { get; }
    }
}
