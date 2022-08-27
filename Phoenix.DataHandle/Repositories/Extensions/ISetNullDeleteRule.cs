using Phoenix.DataHandle.Main.Models.Extensions;

namespace Phoenix.DataHandle.Repositories.Extensions
{
    public interface ISetNullDeleteRule<TModel>
        where TModel : class, IModelEntity
    {
        void SetNullOnDelete(TModel model);
    }
}
