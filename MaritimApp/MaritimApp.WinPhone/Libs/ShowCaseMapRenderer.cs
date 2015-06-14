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
using Xamarin.Forms.Maps.WP8;

[assembly: ExportRenderer(typeof(ShowCaseMap), typeof(ShowCaseMapRenderer))]

namespace MaritimApp.WinPhone.Libs
{
    public class ShowCaseMapRenderer : MapRenderer
    {
        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Control.IsHitTestVisible = false;
            base.OnElementPropertyChanged(sender, e);
            MarkWithPin(Control.Center);
        }

        protected void MarkWithPin(GeoCoordinate coordinate)
        {
            //add new map pushpin
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
