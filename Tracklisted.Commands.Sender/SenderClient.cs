using Microsoft.Azure.ServiceBus;
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
            where T : ICommand
        {
            var message = new Message(Encoding.UTF8.GetBytes(messageBody.CommandId));
            await _client.SendAsync(message);
        }
    }
}
