using HandTelegram.Bot.Infra.Integrations.Abstractions.WeatherForecast;

namespace HandTelegram.Bot.Infra.Integrations.Services.Interfaces
{
    public interface IGoodMorningApiIntegrationService
    {
        Task<ForecastResponseDto> GetForecast(string cityName);
    }
}