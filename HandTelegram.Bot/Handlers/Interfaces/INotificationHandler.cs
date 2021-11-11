using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace HandTelegram.Bot.Handlers.Interfaces
{
    public interface INotificationHandler
    {
        Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken);
        Task HandleNewNotificationAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
    }
}