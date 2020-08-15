using Newtonsoft.Json;

namespace CSGOMarket.Models.General
{
    public class Offer
    {
        public decimal Price { get; set; }

        public int Count { get; set; }

        [JsonProperty("my_count")]
        public int MyCount { get; set; }
    }
}
