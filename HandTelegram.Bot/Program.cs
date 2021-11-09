using HandTelegram.Bot.Worker;
using HandTelegram.Bot.Worker.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace HandTelegram.Bot
{
    class Program
    {
        public static async Task Main()
        {
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            Log.Logger.Information("Application starting");

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<IExampleService, ExampleService>();
                    services.AddTransient<IHandTelegramWorker, HandTelegramWorker>();
                })
                .UseSerilog()
                .Build();

            var svc = ActivatorUtilities.CreateInstance<HandTelegramWorker>(host.Services);
            await svc.StartListen();
        }

        private static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIROMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();

            Environment.SetEnvironmentVariable("TelegramBot:Key", Environment.GetEnvironmentVariable("var_telegram_bot_key"));
        }
    }
}
