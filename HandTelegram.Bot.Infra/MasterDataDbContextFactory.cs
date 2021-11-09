using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace HandTelegram.Bot.Infra.Data
{
    public class MasterDataDbContextFactory : IDesignTimeDbContextFactory<MasterDataDbContext>
    {
        public MasterDataDbContextFactory()
        {

        }

        public MasterDataDbContext CreateDbContext(string[] args)
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../HandTelegram.Bot"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<MasterDataDbContext>();

            Console.WriteLine("string:" + config.GetConnectionString("Default"));

            optionsBuilder
                .UseNpgsql(
                    config.GetConnectionString("Default"),
                    npgSqlServerOptions => npgSqlServerOptions.MigrationsHistoryTable("__EFMigrationsHistory", "masterdata"));

            return new MasterDataDbContext(optionsBuilder.Options);
        }
    }
}
