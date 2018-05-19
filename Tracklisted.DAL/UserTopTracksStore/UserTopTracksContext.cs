using MongoDB.Driver;
using Tracklisted.Configuration;
using Tracklisted.Model.UserTopTracks;

namespace Tracklisted.DAL.UserTopTracksStore
{
    public interface IUserTopTracksContext
    {
        IMongoCollection<UserTopTracks> TopTracksCollection { get; }
    }

    public class UserTopTracksContext : IUserTopTracksContext
    {
        private readonly IMongoDatabase _database = null;

        public UserTopTracksContext(IMongoClient client,
            IMongoConfiguration _connectionConfig)
        {
            _database = client.GetDatabase(_connectionConfig.MongoDatabase);
        }

        public virtual IMongoCollection<UserTopTracks> TopTracksCollection
        {
            get
            {
                return _database.GetCollection<UserTopTracks>("UserTopTracks");
            }
        }
    }
}
