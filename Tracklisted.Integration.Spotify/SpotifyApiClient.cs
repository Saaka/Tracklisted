using System;
using System.Net.Http;
using System.Threading.Tasks;
using Tracklisted.Integration.Spotify.Base;
using Tracklisted.Integration.Spotify.Configuration;

namespace Tracklisted.Integration.Spotify
{
    public interface ISpotifyApiClient
    {
        Task<HttpResponseMessage> CallGetMethod<TRequest>(TRequest request,
            string requestUrl,
            bool useClientAuthorization = true,
            bool useUserAuthorization = false)
            where TRequest : class;
    }
    public class SpotifyApiClient : ISpotifyApiClient
    {
        private readonly HttpClient client;
        private readonly ISpotifyConfiguration spotifyConfig;
        private readonly ISpotifyClientTokenProvider spotifyClientTokenProvider;
        private readonly string baseUrl;

        private const string ClientAuthScheme = "Bearer";

        public SpotifyApiClient(HttpClient client,
            ISpotifyConfiguration spotifyConfig,
            ISpotifyClientTokenProvider spotifyClientTokenProvider)
        {
            this.client = client;
            this.spotifyConfig = spotifyConfig;
            this.spotifyClientTokenProvider = spotifyClientTokenProvider;
            this.baseUrl = spotifyConfig.Url;
        }

        public async Task<HttpResponseMessage> CallGetMethod<TRequest>(TRequest request,
            string requestUrl,
            bool useClientAuthorization = true,
            bool useUserAuthorization = false)
            where TRequest : class
        {
            var httpRequest = await CreateGetRequest(requestUrl, useClientAuthorization);

            return await client.SendAsync(httpRequest);
        }

        private async Task<HttpRequestMessage> CreateGetRequest(string requestUrl,
            bool useClientAuthorization)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{baseUrl}/{requestUrl}")
            };
            if(useClientAuthorization)
            {
                var token = await spotifyClientTokenProvider.GetClientAuthToken();
                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(ClientAuthScheme, token);
            }

            return request;
        }
    }
}
