using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace HandTelegram.Bot.Services.CommandHandlers.Interfaces
{
    public interface ICommandHandler
    {
        Task ForecacastCommandHandler(Message message, string cityName);
    }
}