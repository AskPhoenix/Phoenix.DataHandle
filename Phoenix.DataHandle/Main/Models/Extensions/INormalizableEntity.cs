namespace Phoenix.DataHandle.Main.Models.Extensions
{
    public interface INormalizableEntity<TNormalizableEntity> : IModelEntity
        where TNormalizableEntity : class, IModelEntity
    {
        TNormalizableEntity Normalize();
    }
}
