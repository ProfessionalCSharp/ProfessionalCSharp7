using Microsoft.Extensions.Logging;

namespace LoggingWithoutDI
{
    class Program
    {
        static void Main(string[] args)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddConsole().AddDebug();
            ILogger<Program> logger = loggerFactory.CreateLogger<Program>();
            logger.LogInformation("Info Message");
        }
    }
}
