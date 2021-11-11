using HandTelegram.Bot.Handlers;
using HandTelegram.Bot.Handlers.Interfaces;
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
        private readonly INotificationHandler _notificationHandler;

        public HandTelegramWorker(
            IConfiguration configuration, 
            ILogger<HandTelegramWorker> logger,
            INotificationHandler notificationHandler)
        {
            _configuration = configuration;
            _logger = logger;
            _notificationHandler = notificationHandler;
        }

        public async Task StartListen()
        {
            var bot = new TelegramBotClient(_configuration.GetValue<string>("TelegramBot:Key"));

            using var cts = new CancellationTokenSource();
            bot.StartReceiving(new DefaultUpdateHandler(_notificationHandler.HandleNewNotificationAsync, _notificationHandler.HandleErrorAsync), cts.Token);

            _logger.LogInformation("Listener started.");

            Console.ReadLine();
            cts.Cancel();
        }
    }
}
