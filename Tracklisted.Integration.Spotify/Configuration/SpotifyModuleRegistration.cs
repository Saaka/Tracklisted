using Microsoft.Extensions.DependencyInjection;
using Tracklisted.Infrastructure.Configuration;
using Tracklisted.Integration.Spotify.Base;

namespace Tracklisted.Integration.Spotify.Configuration
{
    public static class SpotifyModuleRegistration
    {
        public static IServiceCollection RegisterSpotifyModule(this IServiceCollection services)
        {
            services
                .RegisterHttpClient<SpotifyApiClient>()
                .RegisterHttpClient<SpotifyAuthApiClient>()
                .AddTransient<ISpotifyClientTokenProvider, SpotifyClientTokenProvider>();

            return services;
        }
    }
}
