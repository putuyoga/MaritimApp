using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldWeatherOnlineAPI.Responses.MarineWeather
{
    public class MarineWeatherNearestArea
    {
        /// <summary>
        /// Distance of nearest area in miles
        /// </summary>
        [JsonProperty("distance_miles")]
        public string DistanceMiles { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }
    }
}
