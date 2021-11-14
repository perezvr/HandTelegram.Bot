using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandTelegram.Bot.Infra.Integrations.Abstractions.GetWeather
{
    public class ForecastDto
    {
        public string date { get; set; }
        public string weekday { get; set; }
        public int max { get; set; }
        public int min { get; set; }
        public string description { get; set; }
        public string condition { get; set; }
    }
}
