using Phoenix.DataHandle.Base;

namespace Phoenix.DataHandle.Main.Entities
{
    public interface IMaterial : IMaterialBase
    {
        IExam Exam { get; }
        IBook? Book { get; }
    }
}
