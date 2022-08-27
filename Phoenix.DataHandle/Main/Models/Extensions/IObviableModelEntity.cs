namespace Phoenix.DataHandle.Main.Models.Extensions
{
    public interface IObviableModelEntity : IModelEntity
    {
        DateTime? ObviatedAt { get; set; }
    }
}
