using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Tracklisted.Configuration;
using Tracklisted.DAL.UserTopTracksStore;

namespace Tracklisted.DAL.Configuration
{
    public static class TracklistedDataModuleRegistration
    {
        public static IServiceCollection RegisterDataModule(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddTransient<IUserTopTracksContext, UserTopTracksContext>()
                .AddTransient<IUserTopTracksRepository, UserTopTracksRepository>();

            MongoDefaults.GuidRepresentation = MongoDB.Bson.GuidRepresentation.Standard;
            var client = new MongoClient(GetMongoConnectionString(configuration));
            services
                .AddSingleton<IMongoClient>(client);

            return services;
        }

        private static string GetMongoConnectionString(IConfiguration configuration)
        {
            return configuration[ConfigurationProperties.Mongo.ConnectionString].ToString();
        }
    }
}
