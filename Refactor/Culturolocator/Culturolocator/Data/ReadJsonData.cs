using Culturolocator.Models.DTO;
using Newtonsoft.Json;

namespace Culturolocator.Data
{
    public class ReadJsonData
    {
        private readonly string JsonPath;

        public ReadJsonData()
        {
            JsonPath = "Data\\JsonData\\historical_cultural_objects.json";
        }

        public List<ObjectDTO> ReadDataFromJson()
        {
            using StreamReader reader = new(JsonPath);
            var json = reader.ReadToEnd();
            List<ObjectDTO> objects = JsonConvert.DeserializeObject<List<ObjectDTO>>(json);
            return objects;
        }
    }
}
