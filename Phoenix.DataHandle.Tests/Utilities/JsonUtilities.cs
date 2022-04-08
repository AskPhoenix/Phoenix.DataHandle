using Newtonsoft.Json;
using Phoenix.DataHandle.Api.Models.Extensions;
using System.IO;

namespace Phoenix.DataHandle.Tests.Utilities
{
    public static class JsonUtilities
    {
        public static void SaveToFile(object modelApi, string dirname, string filename)
        {
            Directory.CreateDirectory(dirname);

            string json = JsonConvert.SerializeObject(modelApi, Formatting.Indented);
            File.WriteAllText($"{dirname}/{filename}.json", json);
        }

        public static IModelApi ReadFromFile<ModelApiT>(string dirname, string filename)
            where ModelApiT : IModelApi
        {
            string json = File.ReadAllText($"{dirname}/{filename}.json");
            return JsonConvert.DeserializeObject<ModelApiT>(json);
        }
    }
}
