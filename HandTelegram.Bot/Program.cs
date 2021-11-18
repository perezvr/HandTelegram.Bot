using HandTelegram.Bot.Config;
using HandTelegram.Bot.Infra.Data;
using HandTelegram.Bot.Infra.Data.Config;
using HandTelegram.Bot.Worker;
using Microsoft.EntityFrameworkCore;
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
        public static void Main()
        {
            var builder = new ConfigurationBuilder();
            var configuration = BuildConfig(builder);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .CreateLogger();

            Log.Logger.Information("Application starting");

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddWorkerServices();
                    services.AddMasterDataDbContext();
                })
                .UseSerilog()
                .Build();

            host.InitializeEFCore();

            var svc = ActivatorUtilities.CreateInstance<HandTelegramWorker>(host.Services);
            svc.StartListen();
        }

        private static IConfiguration BuildConfig(IConfigurationBuilder builder)
        {
            var configuration = builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIROMENT") ?? "production"}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            Environment.SetEnvironmentVariable("TelegramBot:Key", Environment.GetEnvironmentVariable("var_telegram_bot_key"));
            Environment.SetEnvironmentVariable("Postgres:ConnString", Environment.GetEnvironmentVariable("var_postgres_db_conn_string"));

            return configuration;
        }
    }
}
