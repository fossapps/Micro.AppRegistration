using System;
using Micro.AppRegistration.Api.Internal.Configs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Slack;

namespace Micro.AppRegistration.Api.Internal.StartupExtensions
{
    public static class Logger
    {
        public static void ConfigureLoggerWithSlack(this ILoggerFactory loggerFactory, SlackLoggingConfig slackConfig,
            IHostEnvironment env)
        {
            if (string.IsNullOrEmpty(slackConfig.WebhookUrl))
            {
                return;
            }

            loggerFactory.AddSlack(new SlackConfiguration
            {
                MinLevel = slackConfig.MinLogLevel,
                WebhookUrl = new Uri(slackConfig.WebhookUrl)
            }, env.ApplicationName, env.EnvironmentName);
        }
    }
}
