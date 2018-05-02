using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using Tracklisted.Configuration;

namespace Tracklisted.Commands.Sender
{
    internal class SenderClient : IMessageSenderClient
    {
        private readonly IServiceBusConfiguration _configuration;
        private IQueueClient _client;

        public SenderClient(IServiceBusConfiguration configuration)
        {
            _configuration = configuration;
            _client = new QueueClient(_configuration.ServiceBusConnectionString, _configuration.ServiceBusQueue);
        }

        public async Task Send<T>(T messageBody)
            where T : BaseCommand
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messageBody));
            var message = new Message(body);
            message.ContentType = "application/json;charset=utf-8";

            await _client.SendAsync(message);
        }
    }
}
