using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace MaritimApp.Droid
{
    [Activity(Label = "MaritimApp.Droid", Theme = "@style/MaritimTheme", MainLauncher = true, Icon = "@drawable/icon")]
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

            //TODO: For demo set after 5 seconds.
            alarmManager.SetRepeating(AlarmType.ElapsedRealtimeWakeup, SystemClock.ElapsedRealtime() + 5 * 1000, 10000, pendingIntent);
        }
    }
}

