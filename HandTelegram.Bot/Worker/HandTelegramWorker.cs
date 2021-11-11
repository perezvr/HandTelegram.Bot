using HandTelegram.Bot.Handlers;
using HandTelegram.Bot.Worker.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;

namespace HandTelegram.Bot.Worker
{
    public class HandTelegramWorker : IHandTelegramWorker
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<HandTelegramWorker> _logger;

        public HandTelegramWorker(IConfiguration configuration, ILogger<HandTelegramWorker> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task StartListen()
        {
            var bot = new TelegramBotClient(_configuration.GetValue<string>("TelegramBot:Key"));

            var myUser = await bot.GetMeAsync();

            using var cts = new CancellationTokenSource();
            bot.StartReceiving(new DefaultUpdateHandler(NotificationHandler.HandleNewNotificationAsync, NotificationHandler.HandleErrorAsync), cts.Token);

            _logger.LogInformation("Listener started.");

            Console.ReadLine();
            cts.Cancel();
        }
    }
}
