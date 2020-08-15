using System.Net.Http;
using CSGOMarket.Models;

namespace CSGOMarket.Clients
{
    public interface IClient
    {
        HttpClient HttpClient { get; }

        string SecretKey { get; }

        Languages Language { get; set; }
    }
}
