using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldWeatherOnlineAPI.Responses.MarineWeather
{
    public class MarineWeatherData
    {
        [JsonProperty("nearest_area")]
        public List<MarineWeatherNearestArea> NearestArea { get; set; }
        public List<MarineWeatherRequest> Request { get; set; }
        public List<MarineWeather> Weather { get; set; }
    }
}
