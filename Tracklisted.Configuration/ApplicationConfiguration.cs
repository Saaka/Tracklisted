using Microsoft.Extensions.Configuration;

namespace Tracklisted.Configuration
{
    internal class ApplicationConfiguration : IServiceBusConfiguration, IMongoConfiguration
    {
        private readonly IConfiguration _config;

        public ApplicationConfiguration(IConfiguration config)
        {
            _config = config;
        }

        //Mongo
        public string MongoConnectionString => _config[ConfigurationProperties.Mongo.ConnectionString].ToString();
        public string MongoDatabase=> _config[ConfigurationProperties.Mongo.Database].ToString();

        //ServiceBus
        public string ServiceBusConnectionString => _config[ConfigurationProperties.ServiceBus.PrimaryConnectionString].ToString();
        public string ServiceBusSecondaryConnectionString => _config[ConfigurationProperties.ServiceBus.SecondartConnectionString].ToString();
        public string ServiceBusQueue => _config[ConfigurationProperties.ServiceBus.QueueName].ToString();        
    }
}
