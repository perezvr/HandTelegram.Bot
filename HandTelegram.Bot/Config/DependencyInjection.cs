using HandTelegram.Bot.Domain.Interfaces.Repository;
using HandTelegram.Bot.Handlers;
using HandTelegram.Bot.Handlers.Interfaces;
using HandTelegram.Bot.Infra.Data;
using HandTelegram.Bot.Infra.Integrations.Services;
using HandTelegram.Bot.Infra.Integrations.Services.Interfaces;
using HandTelegram.Bot.Infra.Repository;
using HandTelegram.Bot.Services;
using HandTelegram.Bot.Services.CommandHandlers;
using HandTelegram.Bot.Services.CommandHandlers.Interfaces;
using HandTelegram.Bot.Services.Interfaces;
using HandTelegram.Bot.Worker;
using HandTelegram.Bot.Worker.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HandTelegram.Bot.Config
{
    public static class DependencyInjection
    {
        public static void AddMasterDataDbContext(this IServiceCollection services)
        {
            services.AddDbContext<MasterDataDbContext>(options =>
            {
                options.UseNpgsql(
                    Environment.GetEnvironmentVariable("Postgres:ConnString"),
                    npgSqlServerOptions => npgSqlServerOptions.MigrationsHistoryTable("__EFMigrationsHistory", "masterdata"));
            });
        }

        public static void AddWorkerServices(this IServiceCollection services)
        {
            services.AddTransient<IHandTelegramWorker, HandTelegramWorker>();
            services.AddTransient<INotificationHandler, NotificationHandler>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IExampleService, ExampleService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICommandHandler, CommandHandler>();
            services.AddTransient<IGoodMorningApiIntegrationService, GoodMorningApiIntegrationService>();
        }

    }
}
