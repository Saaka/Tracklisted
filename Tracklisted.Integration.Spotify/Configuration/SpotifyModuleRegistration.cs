using Microsoft.Extensions.DependencyInjection;
using Tracklisted.Infrastructure.Configuration;
using Tracklisted.Integration.Spotify.Base;
using Tracklisted.Integration.Spotify.GetSeveralTracks;
using Tracklisted.Integration.Spotify.SearchForTrack;

namespace Tracklisted.Integration.Spotify.Configuration
{
    public static class SpotifyModuleRegistration
    {
        public static IServiceCollection RegisterSpotifyModule(this IServiceCollection services)
        {
            services
                .RegisterHttpClient<SpotifyApiClient>()
                .RegisterHttpClient<SpotifyAuthApiClient>()
                .AddSingleton<ISpotifyConfiguration, SpotifyConfiguration>()
                .AddTransient<ISpotifyClientTokenProvider, SpotifyClientTokenProvider>()
                
                .AddSingleton<IGetSeveralTracksAction, GetSeveralTracksAction>()
                .AddSingleton<ISearchForTrackAction, SearchForTrackAction>();

            return services;
        }
    }
}
