using Phoenix.DataHandle.Main.Models.Extensions;

namespace Phoenix.DataHandle.Repositories.Extensions
{
    public interface ICascadeDeleteRule<TModel>
        where TModel : class, IModelEntity
    {
        Task CascadeOnDeleteAsync(TModel model, CancellationToken cancellationToken = default);
        Task CascadeRangeOnDeleteAsync(IEnumerable<TModel> models, CancellationToken cancellationToken = default);
    }
}
