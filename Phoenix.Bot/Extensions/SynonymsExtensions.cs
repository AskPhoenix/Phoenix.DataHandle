using System;
using System.Linq;

namespace Phoenix.Bot.Extensions
{
    public static class SynonymsExtensions
    {
        public static readonly string[] Greetings = new string[] { "hi", "hello", "γεια", "καλημέρα", "καλησπέρα" };
        
        public enum Topics
        {
            Greetings
        }

        public static bool ContainsSynonyms(this string str, Topics topic)
        {
            var synonyms = typeof(SynonymsExtensions).GetField(topic.ToString()).GetValue(null) as string[];
            
            return str.ToLower().Split(' ').Any(s => synonyms.Contains(s));
        }
    }
}
