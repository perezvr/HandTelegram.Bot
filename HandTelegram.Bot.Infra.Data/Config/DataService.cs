using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandTelegram.Bot.Infra.Data.Config
{
    public static class DataService
    {
        public static void InitializeEFCore(this IHost host)
        {
            Log.Logger.Information("Initializing DB");

            using var serviceScope = host.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<MasterDataDbContext>();

            context
                .Database
                .Migrate();
        }

    }
}
