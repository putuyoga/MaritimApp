//#define DEBUG_AGENT
using System.Diagnostics;
using System.Windows;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using WorldWeatherOnlineAPI;
using WorldWeatherOnlineAPI.Requests;
using MaritimApp.ViewModels;
using System;
using System.Globalization;

namespace BackgroundServiceWP
{
    public class BackgroundService : ScheduledTaskAgent
    {
        /// <remarks>
        /// ScheduledAgent constructor, initializes the UnhandledException handler
        /// </remarks>
        static BackgroundService()
        {
            // Subscribe to the managed exception handler
            Deployment.Current.Dispatcher.BeginInvoke(delegate
            {
                Application.Current.UnhandledException += UnhandledException;
            });

        }


        /// Code to execute on Unhandled Exceptions
        private static void UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                Debugger.Break();
            }
        }

        /// <summary>
        /// Agent that runs a scheduled task
        /// </summary>
        /// <param name="task">
        /// The invoked task
        /// </param>
        /// <remarks>
        /// This method is called when a periodic or resource intensive task is invoked
        /// </remarks>
        protected override void OnInvoke(ScheduledTask task)
        {
            Request();
            
        }

        public void Notify(string judul, string msg, int id, int time_index)
        {
            ShellToast toast = new ShellToast();
            toast.Title = "Peringatan: " + judul;
            toast.NavigationUri = new Uri("/MainPage.xaml?id_lokasi=" + id + "&time_index=" + time_index, UriKind.Relative);
            toast.Content = msg;
            toast.Show();
        }

        public async void Request()
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

            //Ambil lokasi dari database dulu
            LokasiViewModel lokasiVM = new LokasiViewModel();
            lokasiVM.GetListLokasi();

            if(lokasiVM.ListLokasi.Count > 0)
            {
                //Set credential api key World Weather Online
                string apiKey = "e722f3c6f836c840657708c1972ab";
                WorldWeatherOnlineClient client = new WorldWeatherOnlineClient(apiKey);

                foreach(var lokasi in lokasiVM.ListLokasi)
                {
                    WorldWeatherOnlineAPI.Requests.MarineWeatherRequest request = new WorldWeatherOnlineAPI.Requests.MarineWeatherRequest();
                    request.Location = new WorldWeatherOnlineAPI.Commons.Coordinate()
                    {
                        Latitude = lokasi.Latitude,
                        Longitude = lokasi.Longitude
                    };
                    var response = await client.RequestAsync(request);

                    //kalau ga ada respon (internet dll) - skip
                    if (response == null) continue;

                    foreach(var weather in response.Data.Weather[0].Hourly)
                    {
                        //check waktu, jika sudah lewat, skip
                        int waktu = Int32.Parse(weather.Time) / 100;
                        if(DateTime.Now.Hour > waktu) continue;

                        //cek tinggi gelombang
                        float praTinggiGelMaks = float.Parse(weather.SignificantHeightInMeters, CultureInfo.InvariantCulture.NumberFormat);
                        float praTinggiGel = float.Parse(weather.SwellHeightInMeters, CultureInfo.InvariantCulture.NumberFormat);
                        if (praTinggiGel > tinggiGelombangMin || praTinggiGelMaks > tinggiGelombangMin)
                        {
                            Notify(lokasi.Nama, "gelombang @" + waktu + ":00 =" + praTinggiGelMaks + "m", lokasi.ID, waktu / 3);
                            break;
                        }

                        //cek jarak pandang
                        float praJarakPandang = Int32.Parse(weather.Visibility);
                        if(praJarakPandang < jarakPandangMin )
                        {
                            Notify(lokasi.Nama, "jarak pandang @" + waktu + ":00 = " + praJarakPandang + "km", lokasi.ID, waktu / 3);
                            break;
                        }

                        //cek kondisi cuaca
                        int kodeCuaca = Int32.Parse(weather.WeatherCode);
                        if(kondisiCuacaMin == 0 && kodeCuaca > 176)
                        {
                            Notify(lokasi.Nama, "kondisi cuaca kurang baik @" + waktu + ":00", lokasi.ID, waktu / 3);
                            break;
                        }
                        else if(kondisiCuacaMin == 1 && kodeCuaca != 113)
                        {
                            Notify(lokasi.Nama, "kondisi cuaca kurang baik @" + waktu + ":00", lokasi.ID, waktu / 3);
                            break;
                        }
                    }
                }
            }

            // If debugging is enabled, use LaunchForTest to launch the agent in 3 minute.
#if(DEBUG_AGENT)
                    ScheduledActionService.LaunchForTest("BackgroundService", TimeSpan.FromSeconds(180));
#endif

            NotifyComplete();
        }

    }
}