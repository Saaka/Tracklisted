namespace Tracklisted.Configuration
{
    internal class ConfigurationProperties
    {
        public class Mongo
        {
            public const string ConnectionString = "MongoSettings:ConnectionString";
            public const string Database = "MongoSettings:Database";
        }
        public class ServiceBus
        {
            public const string PrimaryConnectionString = "ServiceBusSettings:PrimaryConnectionString";
            public const string SecondartConnectionString = "ServiceBusSettings:SecondaryConnectionString";
            public const string QueueName = "ServiceBusSettings:TracklistedQueueName";
        }
    }
}
