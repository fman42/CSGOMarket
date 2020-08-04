using System;
using System.Net.Http;
using CSGOMarket.Models;

namespace CSGOMarket.Clients
{
    public class AuthClient : IClient
    {
        #region  Var
        public HttpClient HttpClient { get; }

        public string SecretKey { get; }

        public Languages Language { get; set; } = Languages.ru;

        private const string BaseUrl = "https://market.csgo.com/api";
        #endregion

        #region Init
        public AuthClient(string secretKey) : this(secretKey, new HttpClient() {BaseAddress = new Uri(BaseUrl)}) {}

        public AuthClient(string secretKey, string basePoint) : this(secretKey, new HttpClient() { BaseAddress = new Uri(basePoint) }) { }

        public AuthClient(string secretKey, HttpClient client)
        {
            HttpClient = client;
            SecretKey = secretKey;
        }
        #endregion
    }
}
