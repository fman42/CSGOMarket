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
        public async Task<ItemInfo?> GetItemInfoAsync(string itemId)
        {
            string endPoint = $"ItemInfo/{itemId}/{Client.Language}";
            string response = await HttpBuilder.SendRequest(endPoint);

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

        private async Task<bool> SendRequestBuyItemAsync(string endpoint, Dictionary<string, string> args)
            =>  JObject.Parse(await HttpBuilder.SendRequest(endpoint, args))["result"].ToString() == "ok";
        #endregion
    }
}
