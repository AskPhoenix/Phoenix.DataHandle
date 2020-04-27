namespace Phoenix.DataHandle.Main.Entities
{
    public interface IBotFeedback
    {
        IUser Author { get; }
        string Occasion { get; set; }
        byte? Rating { get; set; }
        string Comment { get; set; }
    }
}