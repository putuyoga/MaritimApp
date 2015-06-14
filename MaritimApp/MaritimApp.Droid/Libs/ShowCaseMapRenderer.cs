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
using Xamarin.Forms;
using Android.Gms.Maps;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;
using MaritimApp.Droid.Libs;
using Xamarin.Forms.Maps;
using Android.Gms.Maps.Model;

[assembly: ExportRenderer(typeof(MaritimApp.Libs.ShowCaseMap), typeof(ShowCaseMapRenderer))]

namespace MaritimApp.Droid.Libs
{
    public class ShowCaseMapRenderer : MapRenderer, IOnMapReadyCallback
    {
        private GoogleMap _map;

        public void OnMapReady(GoogleMap googleMap)
        {
            _map = googleMap;

            if (_map != null)
                _map.MapClick += googleMap_MapClick;
            
            //add marker
            var marker = new MarkerOptions();
            marker.SetPosition(_map.CameraPosition.Target);
            marker.InvokeIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.Pin));
            _map.AddMarker(marker);

            //disable gesture & zoom
            _map.UiSettings.ZoomControlsEnabled = false;
            _map.UiSettings.SetAllGesturesEnabled(false);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            if (_map != null)
                _map.MapClick -= googleMap_MapClick;
            base.OnElementChanged(e);

            if (Control != null)
                ((MapView)Control).GetMapAsync(this);
        }

        private void googleMap_MapClick(object sender, GoogleMap.MapClickEventArgs e)
        {
            //((MaritimApp.Libs.ShowCaseMap)Element).OnTap(new Position(e.Point.Latitude, e.Point.Longitude));
        }
    }
}