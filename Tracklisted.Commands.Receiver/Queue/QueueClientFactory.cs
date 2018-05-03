using Microsoft.Azure.ServiceBus;
using System;
using Tracklisted.Configuration;

namespace Tracklisted.Commands.Receiver.Queue
{
    public interface IQueueClientFactory : IDisposable
    {
        IQueueClient GetQueueClient();
    }

    public class QueueClientFactory : IQueueClientFactory
    {
        static IQueueClient queueClient;

        public QueueClientFactory(IServiceBusConfiguration config)
        {
            queueClient = new QueueClient(config.ServiceBusConnectionString, config.ServiceBusQueue);
        }

        public IQueueClient GetQueueClient()
        {
            return queueClient;
        }

        public void Dispose()
        {
            if(queueClient != null)
            {
                queueClient
                    .CloseAsync()
                    .GetAwaiter()
                    .GetResult();
            }
        }
    }
}
