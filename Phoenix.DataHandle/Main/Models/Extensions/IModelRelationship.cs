namespace Phoenix.DataHandle.Main.Models.Extensions
{
    public interface IModelRelationship<out TKey1, out TKey2>
    {
        TKey1 Id1 { get; }
        TKey2 Id2 { get; }
    }

    public interface IModelRelationship : IModelRelationship<int, int> { }
}
