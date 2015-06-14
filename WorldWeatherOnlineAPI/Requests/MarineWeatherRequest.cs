using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldWeatherOnlineAPI.Requests
{
    public class MarineWeatherRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public Commons.Coordinate Location { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Commons.ReturnFormat Format { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Commons.TimeInterval Interval { get; set; }

        
        public string CreateRequestUrl(string url, string apiKey)
        {
            // todo: adjust for premium too
            string output = url + "?format=json&key=" + apiKey + "&q=" + Location.ToString();
            return output;
        }

    }
}
