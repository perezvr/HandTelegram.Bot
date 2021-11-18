using HandTelegram.Bot.Infra.Integrations.Abstractions.WeatherForecast;
using HandTelegram.Bot.Infra.Integrations.Services.Interfaces;
using Newtonsoft.Json;
using RestSharp;

namespace HandTelegram.Bot.Infra.Integrations.Services
{
    public class GoodMorningApiIntegrationService : IGoodMorningApiIntegrationService
    {
        public async Task<ForecastResponseDto> GetForecast(string cityName)
        {
            var client = new RestClient($"http://localhost:8091/weatherforecast");
            var forecastRequest = new RestRequest(Method.GET);

            IRestResponse response = await client.ExecuteAsync(forecastRequest);

            return JsonConvert.DeserializeObject<ForecastResponseDto>(response.Content) ?? new ForecastResponseDto();
        }
    }
}
