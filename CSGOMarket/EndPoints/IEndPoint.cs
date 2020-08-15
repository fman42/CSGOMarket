using CSGOMarket.Clients;

namespace CSGOMarket.EndPoints
{
    public interface IEndPoint
    {
        IClient Client { get; }
    }
}
