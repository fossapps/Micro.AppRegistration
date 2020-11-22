using Microsoft.Extensions.Logging;

namespace Micro.AppRegistration.Configs
{
    public class SlackLoggingConfig
    {
        public string WebhookUrl { set; get; }
        public LogLevel MinLogLevel { set; get; }
    }
}
