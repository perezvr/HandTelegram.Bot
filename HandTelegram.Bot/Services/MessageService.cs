using HandTelegram.Bot.Domain.Interfaces.Repository;
using HandTelegram.Bot.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace HandTelegram.Bot.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;

        public MessageService(
            IUserRepository userRepository,
            ILogger<MessageService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task MessageReceived(
            ITelegramBotClient botClient,
            Message message)
        {
            var user = _userRepository
                .Get(long.Parse(message.From.Id.ToString()));

            if (user == null)
                user = _userRepository
                    .Add(new Domain.Models.User(
                        message.From.Id, 
                        message.From.Username, 
                        message.From.FirstName, 
                        message.From.LastName));

            _logger
                .LogInformation($"Received message: {message.Text} from { message.From.Id}");

            await botClient
                .SendTextMessageAsync(message.Chat.Id, "Oi sumida");
        }
    }
}
