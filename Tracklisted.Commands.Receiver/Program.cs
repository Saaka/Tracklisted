﻿using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tracklisted.Commands.Receiver.Configuration;
using Tracklisted.Configuration;

namespace Tracklisted.Commands.Receiver
{
    class Program
    {
        static IQueueClient queueClient;

        static void Main(string[] args)
        {
            var serviceProvider = DependencyBuilder.Build();

            MainAsync(serviceProvider)
                .GetAwaiter()
                .GetResult();
        }

        static async Task MainAsync(IServiceProvider serviceProvider)
        {
            try
            {
                var config = serviceProvider.GetRequiredService<IServiceBusConfiguration>();
                queueClient = new QueueClient(config.ServiceBusConnectionString, config.ServiceBusQueue);

                Console.WriteLine("===========================");
                Console.WriteLine("Tracklisted client started.");
                Console.WriteLine("============================");

                // Register the queue message handler and receive messages in a loop
                RegisterOnMessageHandlerAndReceiveMessages();

                while (true)
                {
                    Thread.Sleep(1000);
                }
            }
            finally
            {
                await queueClient.CloseAsync();
            }
        }

        static void RegisterOnMessageHandlerAndReceiveMessages()
        {
            // Configure the message handler options in terms of exception handling, number of concurrent messages to deliver, etc.
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                // Maximum number of concurrent calls to the callback ProcessMessagesAsync(), set to 1 for simplicity.
                // Set it according to how many messages the application wants to process in parallel.
                MaxConcurrentCalls = 1,

                // Indicates whether the message pump should automatically complete the messages after returning from user callback.
                // False below indicates the complete operation is handled by the user callback as in ProcessMessagesAsync().
                AutoComplete = false
            };

            // Register the function that processes messages.
            queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }

        static async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            // Process the message.
            Console.WriteLine($"Received message: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{Encoding.UTF8.GetString(message.Body)}");

            // Complete the message so that it is not received again.
            // This can be done only if the queue Client is created in ReceiveMode.PeekLock mode (which is the default).
            await queueClient.CompleteAsync(message.SystemProperties.LockToken);

            // Note: Use the cancellationToken passed as necessary to determine if the queueClient has already been closed.
            // If queueClient has already been closed, you can choose to not call CompleteAsync() or AbandonAsync() etc.
            // to avoid unnecessary exceptions.
        }

        // Use this handler to examine the exceptions received on the message pump.
        static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Entity Path: {context.EntityPath}");
            Console.WriteLine($"- Executing Action: {context.Action}");
            return Task.CompletedTask;
        }
    }
}
