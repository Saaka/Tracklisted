using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Tracklisted.Infrastructure.HttpClient
{
    public interface IJsonResponseContentDeserializer
    {
        Task<TResult> DeserializeContent<TResult>(HttpResponseMessage responseMessage)
            where TResult : class;
    }
    public class JsonResponseContentDeserializer : IJsonResponseContentDeserializer
    {
        public async Task<TResult> DeserializeContent<TResult>(HttpResponseMessage responseMessage)
            where TResult : class
        {
            var responseString = await responseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResult>(responseString);
        }
    }
}
