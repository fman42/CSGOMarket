using Newtonsoft.Json;

namespace CSGOMarket.Models.General
{
    public class Tag
    {
        [JsonProperty("internal_name")]
        public string InternalName { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        [JsonProperty("category_name")]
        public string CategoryName { get; set; }
    }
}
