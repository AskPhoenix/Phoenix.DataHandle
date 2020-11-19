namespace Phoenix.DataHandle.Main
{
    public enum LoginProvider
    {
        Other = -1,
        Emulator,
        Facebook
    }

    public static class LoginProviderExtensions
    {
        public static string GetProviderName(this LoginProvider provider)
        {
            return provider switch
            {
                LoginProvider.Emulator  => "Emulator",
                LoginProvider.Facebook  => "Facebook",
                _                       => string.Empty,
            };
        }

        public static LoginProvider ToLoginProvider(this string channel)
        {
            return channel.ToLower() switch
            {
                "emulator"  => LoginProvider.Emulator,
                "facebook"  => LoginProvider.Facebook,
                _           => LoginProvider.Other
            };
        }
    }
}
