using HandTelegram.Bot.Domain.Interfaces.Repository;
using HandTelegram.Bot.Handlers;
using HandTelegram.Bot.Handlers.Interfaces;
using HandTelegram.Bot.Infra.CrossCutting.IoC;
using HandTelegram.Bot.Infra.Data;
using HandTelegram.Bot.Infra.Repository;
using HandTelegram.Bot.Services;
using HandTelegram.Bot.Services.Interfaces;
using HandTelegram.Bot.Worker;
using HandTelegram.Bot.Worker.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
            var configuration = BuildConfig(builder);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .CreateLogger();

            Log.Logger.Information("Application starting");

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<IHandTelegramWorker, HandTelegramWorker>();
                    services.AddTransient<INotificationHandler, NotificationHandler>();
                    services.AddTransient<IMessageService, MessageService>();
                    services.AddTransient<IExampleService, ExampleService>();
                    services.AddTransient<IUserRepository, UserRepository>();
                    services.AddMasterDataDbContext();
                })
                .UseSerilog()
                .Build();

            using var serviceScope = host.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<MasterDataDbContext>();

            context
                .Database
                .Migrate();

            var svc = ActivatorUtilities.CreateInstance<HandTelegramWorker>(host.Services);
            await svc.StartListen();
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
