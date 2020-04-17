using System;
using System.Linq;

namespace Phoenix.Bot.Extensions
{
    public static class SynonymsExtensions
    {
        //Synonyms arrays with no punctuation
        public static readonly string[] Greetings = new string[] { "hi", "hello", "γεια", "καλημερα", "καλησπερα" };
        public static readonly string[] Help = new string[] { "help", "βοηθεια" };

        public enum Topics
        {
            Greetings,
            Help
        }

        public static bool ContainsSynonyms(this string str, Topics topic)
        {
            var synonyms = typeof(SynonymsExtensions).GetField(topic.ToString()).GetValue(null) as string[];
            
            return str.ToLower().RemovePunctuation().Split(' ').Any(w => synonyms.Contains(w));
        }

        public static string RemovePunctuation(this string str)
        {
            var chars = str.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (char.IsPunctuation(chars[i]))
                {
                    chars[i] = chars[i] switch
                    {
                        'ά' => 'α',
                        'έ' => 'ε',
                        'ή' => 'η',
                        'ό' => 'ο',
                        'ώ' => 'ω',
                        var c when c == 'ί' || c == 'ϊ' || c == 'ΐ' => 'ι',
                        var c when c == 'ύ' || c == 'ϋ' || c == 'ΰ' => 'υ',
                        _   => chars[i]
                    };
                }
            }

            return chars.ToString();
        }
    }
}
