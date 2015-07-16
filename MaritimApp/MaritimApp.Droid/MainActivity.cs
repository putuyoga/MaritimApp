using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using MaritimApp.Droid.Libs;

namespace MaritimApp.Droid
{
    [Activity(Label = "Cuaca Maritim", Theme = "@style/MaritimTheme", MainLauncher = true, Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            Xamarin.FormsMaps.Init(this, bundle);
            LoadApplication(new App());

            //jalankan background service
            addBackgroundService();
        }

        private void addBackgroundService()
        {
            Intent alarmIntent = new Intent(this, typeof(BackgroundService));

            PendingIntent pendingIntent = PendingIntent.GetBroadcast(this, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);
            AlarmManager alarmManager = (AlarmManager)this.GetSystemService(Context.AlarmService);

            //debug mode
            Settings_Android setting = new Settings_Android();
            bool debugMode = setting.GetValues<bool>("debug_mode");
            long interval = 1000 * 60 * 2; //1s * 60 = 1min * 2 = 2 min
            if (!debugMode)
            {
                interval = 1000 * 60 * 30; //1s * 60 = 1min * 30 = 30 min
            }
            alarmManager.SetRepeating(AlarmType.ElapsedRealtimeWakeup, SystemClock.ElapsedRealtime() + 5 * 1000, interval, pendingIntent);
        }
    }
}

