using HandTelegram.Bot.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HandTelegram.Bot.Infra.CrossCutting.IoC
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

    }
}
