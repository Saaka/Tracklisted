using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace Tracklisted.Integration.Spotify.Base
{
    public interface ISpotifyClientTokenProvider
    {
        Task<string> GetClientAuthToken();
    }
    internal class SpotifyClientTokenProvider : ISpotifyClientTokenProvider
    {
        private readonly SpotifyAuthApiClient authApiClient;
        private readonly IMemoryCache memoryCache;

        private const string TokenCacheIdentifier = "SpotifyClientTokenKey";
        private const int TokenExpirationTimeInSeconds = 3600;

        public SpotifyClientTokenProvider(SpotifyAuthApiClient authApiClient,
            IMemoryCache memoryCache)
        {
            this.authApiClient = authApiClient;
            this.memoryCache = memoryCache;
        }

        public async Task<string> GetClientAuthToken()
        {
            var clientAuthToken = await memoryCache.GetOrCreateAsync(TokenCacheIdentifier, async (cacheEntry) =>
            {
                var tokenData = await authApiClient.GetClientAuthorizationToken();

                if (string.IsNullOrEmpty(tokenData.AccessToken))
                {
                    cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);
                    return string.Empty;
                }

                cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(TokenExpirationTimeInSeconds);

                return tokenData.AccessToken;

            });

            return clientAuthToken;
        }
    }
}
