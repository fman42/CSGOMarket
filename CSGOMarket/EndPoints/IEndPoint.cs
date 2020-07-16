using CSGOMarket.Clients;

namespace CSGOMarket.EndPoints
{
    interface IEndPoint
    {
        IClient Client { get; }
    }
}
