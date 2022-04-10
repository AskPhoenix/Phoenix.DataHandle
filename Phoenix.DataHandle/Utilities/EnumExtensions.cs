using System;
using System.Linq;

namespace Phoenix.DataHandle.Utilities
{
    public static class EnumExtensions
    {
        private static string[] GetEnumStrings<TEnum>(string toStringName)
            where TEnum : struct, Enum
        {
            var toSpecialString = typeof(TEnum).GetMethod(toStringName);
            if (toSpecialString == null)
                throw new InvalidOperationException(
                    $"Enumeration {typeof(TEnum)} does not have a {toStringName} method.");

            return Enum.GetValues<TEnum>()
                .Select(v => (string?)toSpecialString.Invoke(v, null))
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Cast<string>()
                .ToArray();
        }

        public static string[] GetFriendlyStrings<TFriendlyEnum>()
            where TFriendlyEnum : struct, Enum
        {
            return GetEnumStrings<TFriendlyEnum>("ToFriendlyString");
        }

        public static string[] GetEmojiStrings<TEmojiEnum>()
            where TEmojiEnum : struct, Enum
        {
            return GetEnumStrings<TEmojiEnum>("ToEmojiString");
        }
    }
}
