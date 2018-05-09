using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tracklisted.Commands.Receiver.CommandHandlers.Infrastructure;

namespace Tracklisted.Commands.Receiver.Queue
{
    public interface IQueueMessageReceiver
    {
        void RegisterOnMessageHandlerAndReceiveMessages();
    }
    public class QueueMessageReceiver : IQueueMessageReceiver
    {
        protected readonly ILogger<QueueMessageReceiver> _logger;
        protected readonly ICommandHandlerFactory _commandHandlerFactory;
        protected readonly ICommandBodyMapper _commandBodyMapper;
        protected readonly IQueueClient _queueClient;

        public QueueMessageReceiver(ILogger<QueueMessageReceiver> logger,
            IQueueClientFactory queueClientFactory,
            ICommandHandlerFactory commandHandlerFactory,
            ICommandBodyMapper commandBodyMapper)
        {
            _logger = logger;
            _commandHandlerFactory = commandHandlerFactory;
            _commandBodyMapper = commandBodyMapper;

            _queueClient = queueClientFactory.GetQueueClient();
        }

        public virtual void RegisterOnMessageHandlerAndReceiveMessages()
        {
            var messageHandlerOptions = GetMessageHandlerOptions();

            _queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }

        protected virtual MessageHandlerOptions GetMessageHandlerOptions()
        {
            return new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 10,
                AutoComplete = false
            };
        }

        protected virtual async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            var body = Encoding.UTF8.GetString(message.Body);
            var baseCommand = _commandBodyMapper
                .MapToBaseCommand(body);

            _logger.LogInformation($"Received message: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{body}");

            var commandHandler = _commandHandlerFactory
                .CreateCommandHandler(baseCommand.CommandType);

            var command = _commandBodyMapper
                .MapToCommand(baseCommand.CommandType, body);

            await commandHandler
                .HandleCommand(command);

            await _queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        protected virtual Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            _logger.LogError($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            _logger.LogError("Exception context for troubleshooting:");
            _logger.LogError($"- Endpoint: {context.Endpoint}");
            _logger.LogError($"- Entity Path: {context.EntityPath}");
            _logger.LogError($"- Executing Action: {context.Action}");
            return Task.CompletedTask;
        }
    }
}
