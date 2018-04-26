using Microsoft.Extensions.DependencyInjection;

namespace Tracklisted.Configuration
{
    public static class ConfigurationRegistrationModule
    {
        /// <summary>
        /// Register dependencies from configuration module
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterConfiguration(this IServiceCollection services)
        {
            services.AddSingleton<IServiceBusConfiguration, ApplicationConfiguration>();
            services.AddSingleton<IMongoConfiguration, ApplicationConfiguration>();

            return services;
        }
    }
}
