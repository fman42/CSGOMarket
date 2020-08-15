using System;
using System.Net.Http;
using CSGOMarket.Models;

namespace CSGOMarket.Clients
{
    public class CSGOMarketAuthClient : IClient
    {
        #region  Var
        public HttpClient HttpClient { get; set;  }

        public string SecretKey { get; }

        public Languages Language { get; set; } = Languages.ru;

        private const string BaseUrl = "https://market.csgo.com/api";
        #endregion

        #region Init
        public CSGOMarketAuthClient(string secretKey) : this(secretKey, new HttpClient() {BaseAddress = new Uri(BaseUrl)}) {}

        public CSGOMarketAuthClient(string secretKey, string basePoint) : this(secretKey, new HttpClient() { BaseAddress = new Uri(basePoint) }) { }

        public CSGOMarketAuthClient(string secretKey, HttpClient client)
        {
            HttpClient = client;
            SecretKey = secretKey;
        }
        #endregion
    }
}
