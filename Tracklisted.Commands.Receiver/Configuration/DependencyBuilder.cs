﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Tracklisted.Commands.Receiver.CommandHandlers;
using Tracklisted.Configuration;

namespace Tracklisted.Commands.Receiver.Configuration
{
    public class DependencyBuilder
    {
        public static IServiceProvider Build()
        {
            var configuration = BuildConfiguration();

            var services = new ServiceCollection()
                .AddSingleton(configuration)
                .AddSingleton<IQueueClientFactory, QueueClientFactory>()
                .AddTransient<ICommandHandlerBase, CommandHandlerBase>()
                .AddLogging(log =>
                {
                    log.AddConsole();
                })
                .RegisterConfiguration();

            return services.
                BuildServiceProvider();
        }

        public static IConfiguration BuildConfiguration()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", false)
                .AddJsonFile($"appsettings.{environment}.json", true)
                .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}
