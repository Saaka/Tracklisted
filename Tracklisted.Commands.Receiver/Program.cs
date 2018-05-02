﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Tracklisted.Commands.Receiver.CommandHandlers;
using Tracklisted.Commands.Receiver.Configuration;

namespace Tracklisted.Commands.Receiver
{
    class Program
    {
        static ILogger logger;

        static void Main(string[] args)
        {
            var serviceProvider = DependencyBuilder.Build();

            MainAsync(serviceProvider)
                .GetAwaiter()
                .GetResult();
        }

        static async Task MainAsync(IServiceProvider serviceProvider)
        {
            logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            var commandHandler = serviceProvider.GetRequiredService<ICommandHandlerBase>();

            logger.LogInformation("===========================");
            logger.LogInformation("Tracklisted client started.");
            logger.LogInformation("============================");

            commandHandler.
                RegisterOnMessageHandlerAndReceiveMessages();

            while (true)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
