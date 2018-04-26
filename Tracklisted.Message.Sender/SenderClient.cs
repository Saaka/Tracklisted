using Microsoft.Azure.ServiceBus;
using System.Text;
using System.Threading.Tasks;
using Tracklisted.Configuration;

namespace Tracklisted.Messages.Sender
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

        public async Task SendMessage<T>(T messageBody)
            where T : IMessage
        {
            var message = new Message(Encoding.UTF8.GetBytes(messageBody.MessageId));
            await _client.SendAsync(message);
        }
    }
}
