using MaritimApp.Helpers;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using WorldWeatherOnlineAPI;
using System.Globalization;

namespace MaritimApp.Models
{
    public class Lokasi
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Nama { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime CuacaLastUpdate { get; set; }

        public async Task<List<Cuaca>> GetPrakiraanCuaca()
        {
            List<Cuaca> NewListCuaca = new List<Cuaca>();
            Database db = new Database();
            var sec = DateTime.Now.Date.Subtract(CuacaLastUpdate).TotalSeconds;

            //Get cache from database ?
            if(sec == 0)
            {
                string sql = String.Format("SELECT * FROM Cuaca WHERE IDLokasi = '{0}'", ID);
                NewListCuaca = db.Query<Cuaca>(sql).ToList();
            }
            else
            {
                //Setup world weather online client
                string apiKey = "adde5021bf1d5fe6ce0d42465c554";
                WorldWeatherOnlineClient client = new WorldWeatherOnlineClient(apiKey);

                //request to world weather online service
                WorldWeatherOnlineAPI.Requests.MarineWeatherRequest request = new WorldWeatherOnlineAPI.Requests.MarineWeatherRequest();
                request.Location = new WorldWeatherOnlineAPI.Commons.Coordinate()
                {
                    Latitude = Latitude,
                    Longitude = Longitude
                };
                var response = await client.RequestAsync(request);
                if (response == null) return null;
 
                //Create list objects
                foreach(var item in response.Data.Weather[0].Hourly)
                {
                    var cuaca = new Cuaca()
                    {
                        IDLokasi = ID,
                        ArahAngin = item.WindDirectionIn16Point,
                        JarakPandang = Int32.Parse(item.Visibility),
                        KecepatanAngin = Int32.Parse(item.WindSpeedInKmph),
                        Kelembapan = Int32.Parse(item.Humidity),
                        KodeCuaca = Int32.Parse(item.WeatherCode),
                        TemperaturAir = Int32.Parse(item.WaterTemperatureInCelcius),
                        TinggiGelombangMaks = float.Parse(item.SignificantHeightInMeters, CultureInfo.InvariantCulture.NumberFormat),
                        ArahGelombang = item.SwellDir,
                        TinggiGelombang = float.Parse(item.SwellHeightInMeters, CultureInfo.InvariantCulture.NumberFormat),
                        Waktu = DateTime.Today.AddHours(Int32.Parse(item.Time) / 100)
                    };
                    NewListCuaca.Add(cuaca);
                }

                //Empty Database
                string emptySQL = String.Format("DELETE FROM Cuaca Where IDLokasi = '{0}'", ID);
                db.Query<Cuaca>(emptySQL);

                //Update LastUpdate Lokasi
                this.CuacaLastUpdate = DateTime.Parse(response.Data.Weather[0].Date);
                db.Update<Lokasi>(this);

                //Insert Fresh Data
                db.Insert(NewListCuaca);
            }
            //TODO: add logic code here
            return NewListCuaca;
        }

        public Cuaca GetCuacaTerdekat()
        {
            Database db = new Database();
            string sql = String.Format("SELECT * FROM Cuaca WHERE (julianday('now', 'localtime') - julianday(Waktu)) > 0 AND IDLokasi = '{0}' ORDER BY Waktu DESC LIMIT 1", ID);
            var result = db.Query<Cuaca>(sql).FirstOrDefault();
            return result;
        }
    }
}
