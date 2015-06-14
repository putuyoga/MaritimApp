using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace WorldWeatherOnlineAPI.Commons
{
    public class Coordinate
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public override string ToString()
        {
            var cult = new CultureInfo("en-US");
            string lat = Latitude.ToString(cult);
            string longi = Longitude.ToString(cult);
            return lat + "," + longi;
        }
    }
}
