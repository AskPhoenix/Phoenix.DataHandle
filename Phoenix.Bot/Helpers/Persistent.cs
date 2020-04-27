namespace Phoenix.Bot.Helpers
{
    public static class Persistent
    {
        public static bool TryGetCommand(string text, out Command command)
        {
            command = text switch
            {
                "--persistent-get-started--"    => Command.GetStarted,
                "--persistent-home--"           => Command.Home,
                "--persistent-tutorial--"       => Command.Tutorial,
                "--persistent-feedback--"       => Command.Feedback,
                _                               => Command.NoCommand
            };

            return command >= 0;
        }

        public static bool IsCommand(string text) => text.StartsWith("--persistent-") && text.EndsWith("--");

        public enum Command
        {
            NoCommand = -1,
            GetStarted,
            Home,
            Tutorial,
            Feedback
        }
    }
}
