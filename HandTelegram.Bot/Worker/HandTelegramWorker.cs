using HandTelegram.Bot.Handlers;
using HandTelegram.Bot.Worker.Interfaces;
using Microsoft.Extensions.Configuration;
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

        public HandTelegramWorker(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task StartListen()
        {
            var bot = new TelegramBotClient(_configuration.GetValue<string>("TelegramBot:Key"));

            var myUser = await bot.GetMeAsync();

            using var cts = new CancellationTokenSource();
            bot.StartReceiving(new DefaultUpdateHandler(NotificationHandler.HandleNewNotificationAsync, NotificationHandler.HandleErrorAsync), cts.Token);
            Console.ReadLine();
            cts.Cancel();
        }
    }
}
