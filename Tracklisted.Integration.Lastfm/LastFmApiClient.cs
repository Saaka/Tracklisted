using System;
using System.Net.Http;
using System.Threading.Tasks;
using Tracklisted.Integration.Lastfm.Base.Request;
using Tracklisted.Integration.Lastfm.Configuration;

namespace Tracklisted.Integration.Lastfm
{
    public class LastfmApiClient
    {
        private readonly HttpClient _client;
        private readonly ILastfmConfiguration _lastfmConfig;
        private readonly string _apiKey;

        public LastfmApiClient(HttpClient client,
            ILastfmConfiguration lastfmConfig)
        {
            _client = client;
            _lastfmConfig = lastfmConfig;

            _apiKey = lastfmConfig.ApiKey;
            _client.BaseAddress = new Uri(lastfmConfig.Url);
        }

        public async Task<HttpResponseMessage> CallGetMethod<TRequest>(TRequest request, string parameters)
            where TRequest : class
        {
            string requestUrl = AddRequestParameters(request);
            return await _client.GetAsync(requestUrl);
        }

        protected string AddRequestParameters<TRequest>(TRequest request)
            where TRequest : class
        {
            string requestUrl = string.Empty;
            requestUrl = AddPageableParameters(request as IPageable, requestUrl);
            requestUrl = $"{requestUrl}&format=json&api_key={_apiKey}";

            return requestUrl;
        }

        protected string AddPageableParameters(IPageable request, string requestUrl)
        {
            if (request == null) return requestUrl;

            return $"{requestUrl}&limit={request.Limit}&page={request.Page}";
        }
    }
}
