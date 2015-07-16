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
using MaritimApp.Droid.Libs;

namespace MaritimApp.Droid
{
    [BroadcastReceiver]
    public class BootReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {

            if(intent.Action.Equals("android.intent.action.BOOT_COMPLETED"))
            {
                Intent alarmIntent = new Intent(context, typeof(BackgroundService));

                PendingIntent pendingIntent = PendingIntent.GetBroadcast(context, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);
                AlarmManager alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);

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
}