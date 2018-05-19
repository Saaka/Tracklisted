using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Tracklisted.Commands.Receiver.Queue;
using Tracklisted.Configuration;
using Tracklisted.DAL.Configuration;
using Tracklisted.Infrastructure.Configuration;
using Tracklisted.Integration.Lastfm.Configuration;
using Tracklisted.Integration.Spotify.Configuration;

namespace Tracklisted.Commands.Receiver.Configuration
{
    public class DependencyBuilder
    {
        const string EnvironmentVariableName = "ASPNETCORE_ENVIRONMENT";

        public static IServiceProvider Build()
        {
            var configuration = BuildConfiguration();

            var services = new ServiceCollection()
                .AddSingleton(configuration)
                .AddSingleton<IQueueClientFactory, QueueClientFactory>()
                .AddTransient<IQueueMessageReceiver, QueueMessageReceiver>()
                .RegisterHttpClientHelpers()
                .RegisterTracklistedModule()
                .RegisterLastfmModule(configuration)
                .RegisterDataModule(configuration)
                .RegisterSpotifyModule()
                .RegisterCommandHandlers()
                .AddLogging(log =>
                {
                    log.AddConsole();
                })
                .AddMemoryCache()
                .RegisterConfiguration();

            return services.
                BuildServiceProvider();
        }

        public static IConfiguration BuildConfiguration()
        {
            var environment = Environment.GetEnvironmentVariable(EnvironmentVariableName);
            
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", false)
                .AddJsonFile($"appsettings.{environment}.json", true)
                .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}
