using Microsoft.Extensions.DependencyInjection;
using Tracklisted.SongSearch.Spotify;

namespace Tracklisted.Configuration
{
    public static class TracklistedModuleRegistration
    {
        public static IServiceCollection RegisterTracklistedModule(this IServiceCollection services)
        {
            services
                .AddTransient<ISpotifySongSearchHandler, SpotifySongSearchHandler>();

            return services;
        }
    }
}
