using MongoDB.Driver;

namespace PdCdpDemoPayment.Data;

public interface IMongoDbClientFactory
{
    protected IMongoClient CreateClient();

    IMongoCollection<T> GetCollection<T>(string collection);
}