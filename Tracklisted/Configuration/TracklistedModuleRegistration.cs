using Microsoft.Extensions.DependencyInjection;
using Tracklisted.Queries.GetUserTopTracksList;
using Tracklisted.SongSearch.Spotify;

namespace Tracklisted.Configuration
{
    public static class TracklistedModuleRegistration
    {
        public static IServiceCollection RegisterTracklistedQueryModule(this IServiceCollection services)
        {
            services
                .AddTransient<IGetUserTopTracksListQuery, GetUserTopTracksListQuery>();

            return services;
        }

        public static IServiceCollection RegisterTracklistedModule(this IServiceCollection services)
        {
            services
                .AddTransient<ISpotifySongSearchHandler, SpotifySongSearchHandler>();

            return services;
        }
    }
}
