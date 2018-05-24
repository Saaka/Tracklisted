using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Tracklisted.Configuration;
using Tracklisted.Infrastructure.Configuration;

namespace Tracklisted.Tests
{
    public abstract class BaseFixture : IDisposable
    {
        public readonly ServiceProvider ServiceProvider;

        public BaseFixture()
        {
            var configuration = BuildConfiguration();

            var serviceBuilder = new ServiceCollection();
            RegisterDependencies(serviceBuilder, configuration)
                .RegisterHttpClientHelpers()
                .AddLogging(log =>
                {
                    log.AddConsole();
                })
                .AddMemoryCache()
                .AddSingleton(configuration)
                .RegisterConfigurationModule();

            ServiceProvider = serviceBuilder.BuildServiceProvider();
        }

        public T GetService<T>()
        {
            return ServiceProvider.GetRequiredService<T>();
        }

        protected virtual IConfiguration BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", false)
                .AddJsonFile($"appsettings.Test.json", true);

            return builder.Build();
        }

        protected abstract IServiceCollection RegisterDependencies(IServiceCollection serviceCollection, IConfiguration configuration);

        public void Dispose()
        {
            if (ServiceProvider != null)
            {
                ServiceProvider
                    .Dispose();
            }
        }
    }
}
