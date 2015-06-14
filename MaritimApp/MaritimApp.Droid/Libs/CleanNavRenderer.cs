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
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Graphics.Drawables;
using MaritimApp.Droid.Libs;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(CleanNavRenderer))]

namespace MaritimApp.Droid.Libs
{
    public class CleanNavRenderer : NavigationRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<NavigationPage> e)
        {
            base.OnElementChanged(e);
            if (Android.OS.Build.VERSION.SdkInt > Android.OS.BuildVersionCodes.Kitkat)
            {
                RemoveAppIconFromActionBar();
            }
        }

        void RemoveAppIconFromActionBar()
        {
            // http://stackoverflow.com/questions/14606294/remove-icon-logo-from-action-bar-on-android
            var actionBar = ((Activity)Context).ActionBar;
            actionBar.SetDisplayShowTitleEnabled(true);
            actionBar.SetDisplayShowHomeEnabled(false);
            actionBar.SetHomeAsUpIndicator(null);
            actionBar.SetHomeButtonEnabled(false);
            actionBar.SetIcon(new ColorDrawable(Color.Transparent.ToAndroid()));

        }
    }
}