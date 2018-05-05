using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Tracklisted.Integration.Lastfm.Base
{
    public abstract class BaseAction<TRequest, TResult> : IAction<TRequest, TResult>
        where TRequest : class
        where TResult : class
    {
        protected readonly LastfmApiClient _apiClient;
        protected readonly DataContractJsonSerializer _serializer;

        public BaseAction(LastfmApiClient apiClient)
        {
            _apiClient = apiClient;
            _serializer = new DataContractJsonSerializer(typeof(TResult));
        }

        public abstract Task<TResult> Execute(TRequest request);

        public async Task<TResult> GetSerializedResponse(HttpResponseMessage responseMessage)
        {
            var responseString = await responseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResult>(responseString);
        }
    }
}
