using Newtonsoft.Json;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace Tracklisted.Infrastructure.Actions
{
    public abstract class BaseHttpAction<TRequest, TResult>
        where TRequest : class
        where TResult : class
    {
        protected readonly DataContractJsonSerializer _serializer;

        public BaseHttpAction()
        {
            _serializer = new DataContractJsonSerializer(typeof(TResult));
        }
       
        protected async Task<TResult> GetSerializedResponse(HttpResponseMessage responseMessage)
        {
            var responseString = await responseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResult>(responseString);
        }
    }
}
