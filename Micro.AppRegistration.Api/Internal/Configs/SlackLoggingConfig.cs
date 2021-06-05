using Microsoft.Extensions.Logging;

namespace Micro.AppRegistration.Api.Internal.Configs
{
    public class SlackLoggingConfig
    {
        public string WebhookUrl { set; get; }
        public LogLevel MinLogLevel { set; get; }
    }
}
