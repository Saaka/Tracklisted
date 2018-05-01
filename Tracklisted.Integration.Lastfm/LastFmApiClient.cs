using System;
using System.Net.Http;
using Tracklisted.Integration.Lastfm.Configuration;

namespace Tracklisted.Integration.Lastfm
{
    public class LastFmApiClient
    {
        private readonly HttpClient _client;
        private readonly ILastfmConfiguration _lastfmConfig;
        private readonly string _apiKey;

        public LastFmApiClient(HttpClient client,
            ILastfmConfiguration lastfmConfig)
        {
            _client = client;
            _lastfmConfig = lastfmConfig;

            _apiKey = lastfmConfig.ApiKey;
            _client.BaseAddress = new Uri(lastfmConfig.Url);
        }
    }
}
