using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HandTelegram.Bot
{
    public class ExampleService : IExampleService
    {
        private readonly ILogger<ExampleService> _log;
        private readonly IConfiguration _configuration;

        public ExampleService(ILogger<ExampleService> log, IConfiguration configuration)
        {
            _log = log;
            _configuration = configuration;
        }

        public void Run()
        {
            for (int i = 0; i < _configuration.GetValue<int>("LoopTimes"); i++)
            {
                _log.LogInformation("Run number {runNumber}", i);
            }
        }
    }
}
