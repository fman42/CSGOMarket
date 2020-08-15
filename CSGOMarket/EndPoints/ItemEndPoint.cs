using CSGOMarket.Clients;
using CSGOMarket.Models;
using CSGOMarket.Tools;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSGOMarket.EndPoints
{
    public class ItemEndPoint : IEndPoint
    {
        #region Var
        public IClient Client { get; }

        private HttpBuilder HttpBuilder { get; }
        #endregion

        #region Init
        public ItemEndPoint(CSGOMarketAuthClient client)
        {
            Client = client;
            HttpBuilder = new HttpBuilder(client);
        }
        #endregion

        #region Public methods
        public async Task<ItemInfo?> GetItemInfoAsync(string itemId)
        {
            string endPoint = $"ItemInfo/{itemId}/{Client.Language}";
            string? response = await HttpBuilder.SendRequest(endPoint);

            if (response is null)
                return null;

            JObject downloadedObject = JObject.Parse(response);
            return downloadedObject.ContainsKey("result") ? null : downloadedObject.ToObject<ItemInfo>();
        }

        public async Task<bool> BuyItemAsync(string itemId, decimal price, long steamId, string token)
        {
            return await SendRequestBuyItemAsync($"Buy/{itemId}/{price}", new Dictionary<string, string>
            {
                { "partner", steamId.ToString()},
                { "token", token }
            });
        }

        public async Task<long> GetBestSellOffer(string itemId) => await GetBestOffer(itemId, "Sell");

        public async Task<long> GetBestBuyOffer(string itemId) => await GetBestOffer(itemId, "Buy");
        #endregion

        #region Private methods
        private async Task<bool> SendRequestBuyItemAsync(string endpoint, Dictionary<string, string> args)
            => JObject.Parse(await HttpBuilder.SendRequest(endpoint, args))["result"].ToString() == "ok";

        private async Task<long> GetBestOffer(string itemId, string typeOffer)
        {
            string? response = await HttpBuilder.SendRequest($"Best{typeOffer}Offer/{itemId}");
            if (response is null)
                return -1;

            JObject parsedResponse = JObject.Parse(response);
            return parsedResponse.Value<bool>("success") ? parsedResponse.Value<long>("best_offer") : -1;
        }
        #endregion
    }
}
