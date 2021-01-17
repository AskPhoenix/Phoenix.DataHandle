using Phoenix.DataHandle.Main.Entities;

namespace Phoenix.DataHandle.Main.Relationships
{
    public interface IParenthood
    {
        IAspNetUsers Child { get; }
        IAspNetUsers Parent { get; }
    }
}
