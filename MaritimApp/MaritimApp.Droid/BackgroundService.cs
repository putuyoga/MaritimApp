using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using Android.Graphics;
using MaritimApp.Helpers;
using MaritimApp.ViewModels;
using WorldWeatherOnlineAPI;
using System.Globalization;

namespace MaritimApp.Droid
{
    [BroadcastReceiver]
    public class BackgroundService : BroadcastReceiver
    {
        public async override void OnReceive(Context context, Intent intent)
        {
            //Ambil pengaturan terlebih dahulu
            PengaturanViewModel pengaturanVM = new PengaturanViewModel();
            float settingJarakPandangMin = pengaturanVM.GetValues<float>("JarakPandangMin");
            float settingTinggiGelombang = pengaturanVM.GetValues<float>("TinggiGelombangMin");
            int settingKondisiCuaca = pengaturanVM.GetValues<int>("KondisiCuacaMin");

            //jika nilai kosong, buat nilai default
            float jarakPandangMin = settingJarakPandangMin == 0 ? 6 : settingJarakPandangMin;
            float tinggiGelombangMin = settingTinggiGelombang == 0 ? 2 : settingTinggiGelombang;
            int kondisiCuacaMin = settingKondisiCuaca;

            //Set credential api key World Weather Online
            string apiKey = "adde5021bf1d5fe6ce0d42465c554";
            WorldWeatherOnlineClient client = new WorldWeatherOnlineClient(apiKey);

            //Ambil lokasi dari database dulu
            LokasiViewModel lokasiVM = new LokasiViewModel();
            lokasiVM.GetListLokasi();

            if (lokasiVM.ListLokasi != null && lokasiVM.ListLokasi.Count > 0)
            {
                foreach (var lokasi in lokasiVM.ListLokasi)
                {
                    WorldWeatherOnlineAPI.Requests.MarineWeatherRequest request = new WorldWeatherOnlineAPI.Requests.MarineWeatherRequest();
                    request.Location = new WorldWeatherOnlineAPI.Commons.Coordinate()
                    {
                        Latitude = lokasi.Latitude,
                        Longitude = lokasi.Longitude
                    };
                    var response = await client.RequestAsync(request);

                    foreach (var weather in response.Data.Weather[0].Hourly)
                    {
                        //check waktu, jika sudah lewat, skip
                        int waktu = Int32.Parse(weather.Time) / 100;
                        if (DateTime.Now.Hour > waktu) continue;

                        //cek tinggi gelombang
                        float praTinggiGelMaks = float.Parse(weather.SignificantHeightInMeters, CultureInfo.InvariantCulture.NumberFormat);
                        float praTinggiGel = float.Parse(weather.SwellHeightInMeters, CultureInfo.InvariantCulture.NumberFormat);
                        if (praTinggiGel > tinggiGelombangMin || praTinggiGelMaks > tinggiGelombangMin)
                        {
                            Notify(lokasi.Nama, "gelombang @" + waktu + ":00 =" + praTinggiGelMaks + "m", context, intent);
                        }

                        //cek jarak pandang
                        float praJarakPandang = Int32.Parse(weather.Visibility);
                        if (praJarakPandang < jarakPandangMin)
                        {
                            Notify(lokasi.Nama, "jarak pandang @" + waktu + ":00 = " + praJarakPandang + "km", context, intent);
                        }

                        //cek kondisi cuaca
                        int kodeCuaca = Int32.Parse(weather.WeatherCode);
                        if (kondisiCuacaMin == 0 && kodeCuaca > 176)
                        {
                            Notify(lokasi.Nama, "kondisi cuaca kurang baik @" + waktu + ":00", context, intent);
                        }
                        else if (kondisiCuacaMin == 1 && kodeCuaca != 113)
                        {
                            Notify(lokasi.Nama, "kondisi cuaca kurang baik @" + waktu + ":00", context, intent);
                        }
                    }
                }
            }
        }

        public void Notify(string title, string message, Context context, Intent intent)
        {
            var notIntent = new Intent(context, typeof(MainActivity));
            var contentIntent = PendingIntent.GetActivity(context, 0, notIntent, PendingIntentFlags.UpdateCurrent);
            var manager = NotificationManagerCompat.From(context);

            var style = new NotificationCompat.BigTextStyle();
            style.BigText(message);

            //Generate a notification with just short text and small icon
            var builder = new NotificationCompat.Builder(context)
                            .SetContentIntent(contentIntent)
                            .SetSmallIcon(Resource.Drawable.Pin)
                            .SetContentTitle(title)
                            .SetContentText(message)
                            .SetStyle(style)
                            .SetWhen(Java.Lang.JavaSystem.CurrentTimeMillis())
                            .SetAutoCancel(true);


            var notification = builder.Build();

            Random random = new Random();
            int m = random.Next(9999 - 1000) + 1000;
            manager.Notify(m, notification);
        }
    }
}