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
        public ItemEndPoint(AuthClient client)
        {
            Client = client;
            HttpBuilder = new HttpBuilder(client);
        }
        #endregion

        #region Methods
        public async Task<ItemInfo?> GetItemInfo(string itemId)
        {
            string endPoint = $"ItemInfo/{itemId}/{Client.Language}";
            JObject downloadedObject = JObject.Parse(await HttpBuilder.SendRequest(endPoint));

            return downloadedObject.ContainsKey("result") ? null : downloadedObject.ToObject<ItemInfo>();
        }

        public async Task<bool> BuyItem(string itemId, decimal price, long steamId)
        {
            return await SendRequestBuyItemAsync($"Buy/{itemId}/{price}", new Dictionary<string, string>
            {
                {"partner", steamId.ToString()}
            });
        }

        public async Task<bool> BuyItem(string itemId, decimal price, string tradeUrl)
        {
            return await SendRequestBuyItemAsync($"Buy/{itemId}/{price}", new Dictionary<string, string>
            {
                {"token", tradeUrl}
            });
        }

        private async Task<bool> SendRequestBuyItemAsync(string endpoint, Dictionary<string, string> args)
            =>  JObject.Parse(await HttpBuilder.SendRequest(endpoint, args))["success"].ToString() == "ok";
        #endregion
    }
}
