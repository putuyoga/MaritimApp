using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using Xamarin.Forms.Maps.Android;
using Android.Gms.Maps;
using MaritimApp.Libs;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using MaritimApp.Droid.Libs;

[assembly: ExportRenderer(typeof(MaritimApp.Libs.ExtendedMap), typeof(ExtendedMapRenderer))]

namespace MaritimApp.Droid.Libs
{
    public class ExtendedMapRenderer : MapRenderer, IOnMapReadyCallback
    {
        private GoogleMap _map;

        public void OnMapReady(GoogleMap googleMap)
        {
            _map = googleMap;
            _map.UiSettings.ZoomControlsEnabled = false;

            if (_map != null)
                _map.MapClick += googleMap_MapClick;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            if (_map != null)
                _map.MapClick -= googleMap_MapClick;
            base.OnElementChanged(e);
            
            if (Control != null)
                ((MapView)Control).GetMapAsync(this);
        }

        private void googleMap_MapClick(object sender, GoogleMap.MapClickEventArgs e)
        {
            ((ExtendedMap)Element).OnTap(new Position(e.Point.Latitude, e.Point.Longitude));
        }
    }
}