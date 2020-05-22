using AdaptiveCards;
using System.Collections.Generic;

namespace Phoenix.Bot.Helpers
{
    public static class CardHelper
    {
        public class AdaptiveTextBlockLight : AdaptiveTextBlock
        {
            public AdaptiveTextBlockLight() : base() 
            {
                this.Color = AdaptiveTextColor.Light;
            }

            public AdaptiveTextBlockLight(string text) : base(text) 
            {
                this.Color = AdaptiveTextColor.Light;
            }
        }

        public class AdaptiveTextBlockHeaderLight : AdaptiveTextBlockLight
        {
            public AdaptiveTextBlockHeaderLight() : base()
            {
                this.Size = AdaptiveTextSize.ExtraLarge;
                this.Weight = AdaptiveTextWeight.Bolder;
            }

            public AdaptiveTextBlockHeaderLight(string text) : base(text)
            {
                this.Size = AdaptiveTextSize.ExtraLarge;
                this.Weight = AdaptiveTextWeight.Bolder;
                this.Wrap = true;
            }
        }

        public class AdaptiveRichFactSetLight : AdaptiveRichTextBlock
        {
            public AdaptiveRichFactSetLight() : base()
            {
                this.Inlines = new List<IAdaptiveInline>()
                    {
                        new AdaptiveTextRun()
                        {
                            Color = AdaptiveTextColor.Light,
                            Size = AdaptiveTextSize.Large,
                            Weight = AdaptiveTextWeight.Bolder
                        },
                        new AdaptiveTextRun()
                        {
                            Color = AdaptiveTextColor.Light,
                            Size = AdaptiveTextSize.Large,
                            Weight = AdaptiveTextWeight.Lighter
                        }
                    };
            }

            public AdaptiveRichFactSetLight(string fact, string value, bool separator = false) : base()
            {
                this.Inlines = new List<IAdaptiveInline>()
                    {
                        new AdaptiveTextRun()
                        {
                            Text = fact,
                            Color = AdaptiveTextColor.Light,
                            Size = AdaptiveTextSize.Large,
                            Weight = AdaptiveTextWeight.Bolder
                        },
                        new AdaptiveTextRun()
                        {
                            Text = value,
                            Color = AdaptiveTextColor.Light,
                            Size = AdaptiveTextSize.Large,
                            Weight = AdaptiveTextWeight.Lighter
                        }
                    };
                this.Separator = separator;
            }
        }
    }
}
