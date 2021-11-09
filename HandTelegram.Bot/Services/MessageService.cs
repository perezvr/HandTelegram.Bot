using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace HandTelegram.Bot.Services
{
    public class MessageService
    {
        public static async Task MessageReceived(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            await botClient.SendTextMessageAsync(message.Chat.Id, "Oi sumida");
        }
    }
}
