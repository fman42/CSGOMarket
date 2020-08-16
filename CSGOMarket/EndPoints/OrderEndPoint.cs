using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSGOMarket.Clients;
using CSGOMarket.Models;
using CSGOMarket.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CSGOMarket.EndPoints
{
    public class OrderEndPoint : IEndPoint
    {
        #region Var
        public IClient Client { get; }

        private HttpBuilder Builder { get; }
        #endregion

        #region Init
        public OrderEndPoint(CSGOMarketAuthClient client) => Builder = new HttpBuilder(client);
        #endregion

        #region Get Order
        public async Task<OrderList?> GetAsync(int page, bool extend = true)
        {
            string? response = await Builder.SendRequest($"GetOrders/{page}", new Dictionary<string, string>() {
                { "extend", (extend ? 1 : 0).ToString() }
            });

            if (response is null)
                return null;

            return JsonConvert.DeserializeObject<OrderList>(response);
        }
        #endregion

        #region Insert Order
        public async Task<bool> InsertAsync(string itemId, long price, string hash = null)
            => await InsertAsync(itemId, price, hash, null, null);

        public async Task<bool> InsertAsync(string itemId, long price, string hash, long steamId, string token)
            => await InsertAsync(itemId, price, hash, steamId, token);

        private async Task<bool> InsertAsync(string itemId, long price, string? hash, long? steamId, string? token)
        {
            string[] itemData = itemId.Split('_');
            if (itemData.Length != 2)
                throw new ArgumentException("itemId has to contain classId and instanceId with separrator _");

            string endPoint = $"InsertOrder/{itemData[0]}/{itemData[1]}/{price}/{hash}";
            string? response = await Builder.SendRequest(endPoint, GetArgumentsForInsert(steamId, token));

            if (response is null)
                return false;

            return JObject.Parse(response).Value<bool>("success");
        }

        private Dictionary<string, string> GetArgumentsForInsert(long? steamId, string? token)
        {
            if (steamId is null || token is null)
                return null;

            return new Dictionary<string, string>()
            {
                { "partner", steamId.ToString() },
                { "token", token }
            };
        }
        #endregion

        #region Delete Order
        public async Task<(bool, int)> DeleteAllAsync()
        {
            string? response = await Builder.SendRequest("DeleteOrders");
            if (response is null)
                return (false, 0);

            JObject parsedResponse = JObject.Parse(response);
            return (parsedResponse.Value<bool>("success"), parsedResponse.Value<int>("deleted_orders"));
        }
        #endregion

        public async Task<(bool online, bool hasItems)?> StatusOrdersAsync()
        {
            string? response = await Builder.SendRequest("StatusOrders");
            if (response is null)
                return null;

            JObject parsedResponse = JObject.Parse(response);
            return (parsedResponse.Value<bool>("online"), parsedResponse.Value<bool>("has_items"));
        }
    }
}
