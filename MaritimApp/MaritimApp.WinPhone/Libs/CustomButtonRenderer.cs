using MaritimApp.WinPhone.Libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Xamarin.Forms.Platform.WinPhone;

[assembly: Xamarin.Forms.ExportRenderer(typeof(Xamarin.Forms.Button), typeof(CustomButtonRenderer))]

namespace MaritimApp.WinPhone.Libs
{
    public class CustomButtonRenderer : ButtonRenderer
    {

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            //ganti warna background *hack-code
            Control.Background = App.Current.Resources["MainColorBrush"] as SolidColorBrush;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            Control.Background = App.Current.Resources["MainColorBrush"] as SolidColorBrush;

            //apply style
            Control.Style = App.Current.Resources["ButtonStyle"] as Style;
        }

    }
}
