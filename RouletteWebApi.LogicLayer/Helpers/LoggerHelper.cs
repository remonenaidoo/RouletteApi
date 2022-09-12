using Microsoft.Extensions.Logging;
using RouletteWebApi.LogicLayer.Helpers.Interfaces;

namespace RouletteWebApi.LogicLayer.Helpers
{
    public class LoggerHelper : ILoggerHelper
    {
        private readonly ILogger<LoggerHelper> _logger;
        public LoggerHelper(ILogger<LoggerHelper> logger)
        {
            _logger = logger;
        }
        public void LogError(string Message)
        {
            _logger.LogError(Message);
        }
        public void LogInfo(string Message)
        {
            _logger.LogInformation(Message);
        }
    }
}
