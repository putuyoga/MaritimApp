using MaritimApp.Libs;
using MaritimApp.WinPhone.Libs;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Maps.Toolkit;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.WP8;
using Xamarin.Forms.Platform.WinPhone;

[assembly: ExportRenderer(typeof(ExtendedMap), typeof(ExtendedMapRenderer))]

namespace MaritimApp.WinPhone.Libs
{
    public class ExtendedMapRenderer : MapRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Maps.Map> e)
        {
            if (Control != null)
                Control.Tap -= Control_Tap;
            base.OnElementChanged(e);
            if (Control != null)
                Control.Tap += Control_Tap;
        }

        private void Control_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var coordinate = Control.ConvertViewportPointToGeoCoordinate(e.GetPosition(Control));
            ((ExtendedMap)Element).OnTap(new Position(coordinate.Latitude, coordinate.Longitude));

            MarkWithPin(new GeoCoordinate(coordinate.Latitude, coordinate.Longitude));
        }

        protected void MarkWithPin(GeoCoordinate coordinate)
        {
            //tambah pusphin baru
            Pushpin pin = new Pushpin();
            pin.GeoCoordinate = coordinate;
            pin.Template = App.Current.Resources["PushPinTemplate"] as System.Windows.Controls.ControlTemplate;

            MapOverlay overlay = new MapOverlay();
            overlay.GeoCoordinate = coordinate;
            overlay.Content = pin;

            MapLayer layer = new MapLayer();
            layer.Add(overlay);
            Control.Layers.Clear();
            Control.Layers.Add(layer);
        }

    }
}
