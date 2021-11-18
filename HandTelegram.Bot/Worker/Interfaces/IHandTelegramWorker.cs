using System.Threading.Tasks;

namespace HandTelegram.Bot.Worker.Interfaces
{
    public interface IHandTelegramWorker
    {
        void StartListen();
    }
}