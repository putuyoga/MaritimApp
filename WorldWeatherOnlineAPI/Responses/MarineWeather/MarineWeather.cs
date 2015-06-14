using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldWeatherOnlineAPI.Responses.MarineWeather
{
    public class MarineWeather
    {
        public string Date { get; set; }
        public List<MarineWeatherHourly> Hourly { get; set; }

        [JsonProperty("maxtempC")]
        public string MaxTemperatureInCelsius { get; set; }

        [JsonProperty("mintempC")]
        public string MinTemperatureInCelsius { get; set; }

    }
}
