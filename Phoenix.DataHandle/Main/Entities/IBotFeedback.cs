namespace Phoenix.DataHandle.Main.Entities
{
    public interface IBotFeedback
    {
        IUser Author { get; }
        string Topic { get; set; }
        byte? Rating { get; set; }
        string Comments { get; set; }
    }
}