using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tracklisted.Commands.Receiver.CommandHandlers
{
    public class CommandHandlerBase
    {
        protected ILogger<CommandHandlerBase> logger;
        protected IQueueClient queueClient;

        public CommandHandlerBase(ILogger<CommandHandlerBase> logger,
            IQueueClientFactory queueClientFactory)
        {
            this.logger = logger;
            this.queueClient = queueClientFactory.GetQueueClient();
        }

        public void RegisterOnMessageHandlerAndReceiveMessages()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 10,
                AutoComplete = false
            };
            
            queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }
    
        protected async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            var body = Encoding.UTF8.GetString(message.Body);
            var command = JsonConvert.DeserializeObject<BaseCommand>(body);
            
            logger.LogInformation($"Received message: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{body}");
            
            await queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        protected Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            logger.LogError($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            logger.LogError("Exception context for troubleshooting:");
            logger.LogError($"- Endpoint: {context.Endpoint}");
            logger.LogError($"- Entity Path: {context.EntityPath}");
            logger.LogError($"- Executing Action: {context.Action}");
            return Task.CompletedTask;
        }
    }
}
