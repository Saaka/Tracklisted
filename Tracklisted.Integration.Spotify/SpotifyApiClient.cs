using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
        private const int DefaultRetryDelayInSeconds = 3;

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

            var response = await client.SendAsync(httpRequest);

            if (response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                await Task.Delay(GetDelay(response.Headers.RetryAfter));
                return await CallGetMethod(request, requestUrl, useClientAuthorization, useUserAuthorization);
            }
            return response;
        }

        private TimeSpan GetDelay(RetryConditionHeaderValue retryAfter)
        {
            if (retryAfter.Delta.HasValue)
                return retryAfter.Delta.Value.Add(TimeSpan.FromSeconds(1));

            return TimeSpan.FromSeconds(DefaultRetryDelayInSeconds);
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
