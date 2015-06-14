using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WorldWeatherOnlineAPI.Requests;
using WorldWeatherOnlineAPI.Responses;

namespace WorldWeatherOnlineAPI
{
    public class WorldWeatherOnlineClient
    {
        /// <summary>
        /// Lokasi
        /// </summary>
        private const string _url = "http://api.worldweatheronline.com/free/v2/marine.ashx";

        public event EventHandler FailedGetData;

        private string _apiKey;
        public WorldWeatherOnlineClient(string ApiKey)
        {
            _apiKey = ApiKey;
        }

        public async Task<MarineWeatherResponse> RequestAsync(MarineWeatherRequest request)
        {
            try
            {
                HttpClient client = new HttpClient();
                string requestUrl = request.CreateRequestUrl(_url, _apiKey);
                Debug.WriteLine("Test");
                string jsonRaw = await client.GetStringAsync(requestUrl);
                var response = JsonConvert.DeserializeObject<MarineWeatherResponse>(jsonRaw);
                return response;
            }
            catch(Exception ex)
            {
                if (FailedGetData != null) FailedGetData(this, new EventArgs());
                return null;
            }
        }
    }
}
