using Newtonsoft.Json;

namespace CSGOMarket.Models
{
    public class Order
    {
        [JsonProperty("i_classid")]
        public string ClassId { get; set; }

        [JsonProperty("i_instanceid")]
        public string InstanceId { get; set; }

        [JsonProperty("i_market_hash_name")]
        public string MarketHashName { get; set; }

        [JsonProperty("i_market_name")]
        public string MarketName { get; set; }

        [JsonProperty("o_price")]
        public long Price { get; set; }

        [JsonProperty("o_state")]
        public long State { get; set; }
    }
}
