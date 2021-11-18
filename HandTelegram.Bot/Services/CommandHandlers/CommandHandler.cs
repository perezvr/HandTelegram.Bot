using HandTelegram.Bot.Infra.Integrations.Services.Interfaces;
using HandTelegram.Bot.Services.CommandHandlers.Interfaces;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace HandTelegram.Bot.Services.CommandHandlers
{
    internal class CommandHandler : ICommandHandler
    {
        private readonly IGoodMorningApiIntegrationService _goodMorningApiIntegrationService;

        public CommandHandler(IGoodMorningApiIntegrationService goodMorningApiIntegrationService)
        {
            _goodMorningApiIntegrationService = goodMorningApiIntegrationService;
        }

        public async Task ForecacastCommandHandler(Message message, string cityName)
        {
            var response = await _goodMorningApiIntegrationService.GetForecast(cityName);
        }
    }
}
