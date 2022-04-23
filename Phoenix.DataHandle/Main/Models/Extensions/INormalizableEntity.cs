namespace Phoenix.DataHandle.Main.Models.Extensions
{
    public interface INormalizableEntity : IModelEntity
    {
        void Normalize();
    }
}
