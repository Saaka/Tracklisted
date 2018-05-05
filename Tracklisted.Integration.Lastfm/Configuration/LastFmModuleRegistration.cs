using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tracklisted.Infrastructure.Configuration;
using Tracklisted.Integration.Lastfm.Base;

namespace Tracklisted.Integration.Lastfm.Configuration
{
    public static class LastFmModuleRegistration
    {
        public static IServiceCollection RegisterLastfmModule(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .RegisterHttpClient<LastfmApiClient>();

            services
                .AddSingleton<ILastfmConfiguration, LastfmConfiguration>()
                .AddSingleton<IPeriodMapper, PeriodMapper>();

            return services;
        }
    }
}
