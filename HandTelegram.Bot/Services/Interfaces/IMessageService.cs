using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace HandTelegram.Bot.Services.Interfaces
{
    public interface IMessageService
    {
        Task MessageReceived(ITelegramBotClient botClient, Message message);
    }
}