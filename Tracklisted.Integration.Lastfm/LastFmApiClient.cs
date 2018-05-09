using System;
using System.Net.Http;
using System.Threading.Tasks;
using Tracklisted.Integration.Lastfm.Base.Request;
using Tracklisted.Integration.Lastfm.Configuration;

namespace Tracklisted.Integration.Lastfm
{
    public interface ILastfmApiClient
    {
        Task<HttpResponseMessage> CallGetMethod<TRequest>(TRequest request, string requestParameters)
            where TRequest : class;
    }

    public class LastfmApiClient : ILastfmApiClient
    {
        private readonly HttpClient client;
        private readonly ILastfmConfiguration lastfmConfig;

        public LastfmApiClient(HttpClient client,
            ILastfmConfiguration lastfmConfig)
        {
            this.client = client;
            this.lastfmConfig = lastfmConfig;
            
            this.client.BaseAddress = new Uri(lastfmConfig.Url);
        }

        public async Task<HttpResponseMessage> CallGetMethod<TRequest>(TRequest request, string requestParameters)
            where TRequest : class
        {
            string requestUrl = AddRequestParameters(request, requestParameters);
            return await client.GetAsync(requestUrl);
        }

        protected string AddRequestParameters<TRequest>(TRequest request, string requestParameters)
            where TRequest : class
        {
            string requestUrl = requestParameters;
            requestUrl = AddPageableParameters(request as IPageable, requestUrl);
            requestUrl = $"{requestUrl}&format=json&api_key={lastfmConfig.ApiKey}";

            return requestUrl;
        }

        protected string AddPageableParameters(IPageable request, string requestUrl)
        {
            if (request == null) return requestUrl;

            return $"{requestUrl}&limit={request.Limit}&page={request.Page}";
        }
    }
}
