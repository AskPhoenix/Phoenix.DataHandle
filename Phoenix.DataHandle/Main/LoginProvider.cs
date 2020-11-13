namespace Phoenix.DataHandle.Main
{
    public enum LoginProvider
    {
        Emulator,
        Facebook
    }

    public static class LoginProviderExtensions
    {
        public static string GetProviderName(this LoginProvider provider)
        {
            switch (provider)
            {
                case LoginProvider.Emulator:
                    return "Emulator";
                case LoginProvider.Facebook:
                    return "Facebook";
                default:
                    return string.Empty;
            }
        }
    }
}
