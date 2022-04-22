using System;
using System.Linq;

namespace Phoenix.DataHandle.Utilities
{
    public static class OTCGenerator
    {
        public static string Generate(OTCGeneratorOptions options = default)
        {
            if (options.IsAlphanumeric)
                return new string(Convert.ToBase64String(Guid.NewGuid().ToByteArray())
                    .Where(c => !options.SkipCharacters.Contains(c) && char.IsLetterOrDigit(c))
                    .ToArray()).Substring(0, options.Length);
             
            if (options.Length > 9)
                throw new InvalidOperationException("The number of digits is too much. Set the Code Length to be less than 10.");

            int min = (int)Math.Pow(10, options.Length - 1);
            int max = min * 10 - 1;

            return new Random().Next(min, max).ToString();
        }
    }

    public struct OTCGeneratorOptions
    {
        public int Length { get; set; }
        public bool IsAlphanumeric { get; set; }
        public char[] SkipCharacters { get; set; }

        public OTCGeneratorOptions()
        {
            this.Length = 4;
            this.IsAlphanumeric = false;
            this.SkipCharacters = new[] { 'l', 'I', 'O', '0', '1' };
        }
    }
}
