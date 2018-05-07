using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tracklisted.Integration.Spotify.Configuration;
using Tracklisted.Integration.Spotify.Models;

namespace Tracklisted.Integration.Spotify
{
    public class SpotifyAuthApiClient
    {
        private readonly HttpClient _client;
        private readonly ISpotifyConfiguration _spotifyConfig;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _authUrl;

        private const string ContentGrantType = "grant_type";
        private const string ContentGrantTypeValue = "client_credentials";
        private const string MediaType = "application/x-www-form-urlencoded";

        public SpotifyAuthApiClient(HttpClient client,
            ISpotifyConfiguration spotifyConfig)
        {
            _client = client;
            _clientId = spotifyConfig.ClientID;
            _clientSecret = spotifyConfig.ClientSecret;
            _authUrl = spotifyConfig.AuthorizationUrl;

            _spotifyConfig = spotifyConfig;
        }

        public async Task<ClientAuthResponse> GetClientAuthorizationToken()
        {
            using (var response = await _client.SendAsync(CreateAuthRequest()))
            {
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<ClientAuthResponse>(responseBody);
                }

                throw new InvalidOperationException(response.ReasonPhrase);
            }
        }

        private HttpRequestMessage CreateAuthRequest()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                Content = new FormUrlEncodedContent(CreateContent()),
                RequestUri = new Uri(_authUrl)
            };
            request.Headers.Clear();
            request.Headers.Accept
                .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(MediaType));
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", CreateAuthHeaderValue());

            return request;
        }

        private string CreateAuthHeaderValue()
        {
            var authData = Encoding.UTF8.GetBytes($"{_clientId}:{_clientSecret}");

            return Convert.ToBase64String(authData);
        }

        private IEnumerable<KeyValuePair<string, string>> CreateContent()
        {
            return new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(ContentGrantType, ContentGrantTypeValue)
            };
        }
    }
}
