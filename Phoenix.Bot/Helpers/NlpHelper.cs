using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Phoenix.Bot.Helpers
{
    public static class NlpHelper
    {
        public static class Wit
        {
            public static Entity GetFbTopEntity(object FbChannelData)
            {
                try
                {
                    var nlp = JObject.FromObject(FbChannelData)["message"]["nlp"] as JObject;

                    Entity entity = JsonConvert.DeserializeObject<Entity>(nlp["entities"].First.First[0].ToString());
                    entity.Name = (nlp["entities"] as JObject).Properties().First().Name;

                    return entity;
                }
                catch
                {
                    return null;
                }
            }

            [JsonObject]
            public class Entity
            {
                public string Name { get; set; }

                [JsonProperty("confidence")]
                public float Confidence { get; set; }

                [JsonProperty("value")]
                public string Value { get; set; }

            }
        }

    }
}
