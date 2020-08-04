using CSGOMarket.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;

namespace CSGOMarket.Tools
{
    internal class HttpBuilder
    {
        private IClient Client { get; }
        public HttpBuilder(IClient client)
        {
            Client = client;
        }

        public async Task<string?> SendRequest(string endpoint, Dictionary<string, string> args = null)
        {
            args ??= new Dictionary<string, string>();

            Uri source = new Uri(Client.HttpClient.BaseAddress + $"/{endpoint}/?{BuildRequestRaw(args)}");
            HttpResponseMessage response = await Client.HttpClient.GetAsync(source.ToString());

            return response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : null;
        }

        private string BuildRequestRaw(Dictionary<string, string> args)
        {
            if (Client is AuthClient) args.Add("key", Client.SecretKey);

            return string.Join('&', args.Select(x =>
                $"{HttpUtility.UrlEncode(x.Key)}={HttpUtility.UrlEncode(x.Value)}"));
        }
    }
}
