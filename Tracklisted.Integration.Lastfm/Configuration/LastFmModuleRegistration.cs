using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tracklisted.Infrastructure.Configuration;

namespace Tracklisted.Integration.Lastfm.Configuration
{
    public static class LastFmModuleRegistration
    {
        public static IServiceCollection RegisterLastfmModule(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .RegisterHttpClient<LastfmApiClient>();

            services
                .AddSingleton<ILastfmConfiguration, LastfmConfiguration>();

            return services;
        }
    }
}
