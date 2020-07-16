using System.Collections.Generic;
using CSGOMarket.Models.General;
using Newtonsoft.Json;

namespace CSGOMarket.Models
{
    public class ItemInfo
    {
        public string Classid { get; set; }

        public string Instanceid { get; set; }

        public string Name { get; set; }

        [JsonProperty("market_hash_name")]
        public string MarketHashName { get; set; }

        public string Rarity { get; set; }

        public string Quality { get; set; }

        public string Type { get; set; }

        public string Mtype { get; set; }

        public string Slot { get; set; }

        public List<Description> Description { get; set; }

        public List<Tag> Tags { get; set; }

        public string Hash { get; set; }

        [JsonProperty("min_price")]
        public decimal MinPrice { get; set; }

        public List<Offer> Offers { get; set; }
    }
}
