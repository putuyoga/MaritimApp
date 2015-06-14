using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldWeatherOnlineAPI.Responses.MarineWeather
{
    public class MarineWeatherHourly
    {
        public string CloudCover { get; set; }
        public string Humidity { get; set; }
        public string PrecipMM { get; set; }
        public string Pressure { get; set; }

        [JsonProperty("sigHeight_m")]
        public string SignificantHeightInMeters { get; set; }

        public string SwellDir { get; set; }

        [JsonProperty("swellHeight_m")]
        public string SwellHeightInMeters { get; set; }

        [JsonProperty("swellPeriod_secs")]
        public string SwellPeriodInSeconds { get; set; }

        [JsonProperty("tempC")]
        public string TemperatureInCelcius { get; set; }

        [JsonProperty("tempF")]
        public string TemperatureInFahrenheit { get; set; }

        public string Time { get; set; }
        public string Visibility { get; set; }

        [JsonProperty("waterTemp_C")]
        public string WaterTemperatureInCelcius { get; set; }

        [JsonProperty("waterTemp_F")]
        public string WaterTemperatureInFahrenheit { get; set; }


        public string WeatherCode { get; set; }
        public List<MarineWeatherDescription> weatherDesc { get; set; }
        public List<MarineWeatherIconUrl> weatherIconUrl { get; set; }

        [JsonProperty("winddir16Point")]
        public string WindDirectionIn16Point { get; set; }

        [JsonProperty("winddirDegree")]
        public string WindDirectionInDegree { get; set; }

        [JsonProperty("windspeedKmph")]
        public string WindSpeedInKmph { get; set; }

        [JsonProperty("windspeedMiles")]
        public string WindspeedInMiles { get; set; }

    }
}
