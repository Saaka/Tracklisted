using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tracklisted.Integration.Spotify.Configuration;

namespace Tracklisted.Integration.Spotify
{
    class SpotifyApiClient
    {
        private readonly HttpClient _client;
        private readonly ISpotifyConfiguration _spotifyConfig;

        public SpotifyApiClient(HttpClient client,
            ISpotifyConfiguration spotifyConfig)
        {
            _client = client;
            _spotifyConfig = spotifyConfig;

            _client.BaseAddress = new Uri(spotifyConfig.Url);
        }

        public async Task<HttpResponseMessage> CallGetMethod<TRequest>(TRequest request, 
            string requestUrl, 
            bool useClientAuthorization = true, 
            bool useUserAuthorization = false)
            where TRequest : class
        {
            return await _client.GetAsync(requestUrl);
        }
    }
}
