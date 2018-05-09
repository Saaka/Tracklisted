using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tracklisted.Infrastructure.Configuration;
using Tracklisted.Integration.Lastfm.Base;
using Tracklisted.Integration.Lastfm.GetUserTopTracks;

namespace Tracklisted.Integration.Lastfm.Configuration
{
    public static class LastfmModuleRegistration
    {
        public static IServiceCollection RegisterLastfmModule(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .RegisterHttpClient<ILastfmApiClient, LastfmApiClient>();

            services
                .AddSingleton<ILastfmConfiguration, LastfmConfiguration>()
                .AddSingleton<IPeriodMapper, PeriodMapper>()
                .AddTransient<IGetUserTopTracksAction, GetUserTopTracksAction>();

            return services;
        }
    }
}
