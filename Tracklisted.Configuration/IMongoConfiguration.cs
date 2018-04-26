namespace Tracklisted.Configuration
{
    public interface IMongoConfiguration
    {
        string MongoConnectionString { get; }
        string MongoDatabase { get; }
    }
}
