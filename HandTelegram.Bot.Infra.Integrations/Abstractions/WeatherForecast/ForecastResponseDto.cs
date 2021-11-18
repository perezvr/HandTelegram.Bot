namespace HandTelegram.Bot.Infra.Integrations.Abstractions.WeatherForecast
{
    public class ForecastResponseDto
    {
        public string by { get; set; }
        public bool valid_key { get; set; }
        public DetailDto results { get; set; }
        public double execution_time { get; set; }
        public bool from_cache { get; set; }
    }
}
