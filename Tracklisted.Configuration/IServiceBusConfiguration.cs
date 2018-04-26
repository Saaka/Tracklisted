namespace Tracklisted.Configuration
{
    public interface IServiceBusConfiguration
    {
        string ServiceBusConnectionString { get; }
        string ServiceBusSecondaryConnectionString { get; }
        string ServiceBusQueue { get; }
    }
}
