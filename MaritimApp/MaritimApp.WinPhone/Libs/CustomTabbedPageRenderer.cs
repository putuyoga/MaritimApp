using MaritimApp.Libs;
using MaritimApp.WinPhone.Libs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;

[assembly: ExportRenderer(typeof(TabbedPage),typeof(CustomTabbedPageRenderer))]

namespace MaritimApp.WinPhone.Libs
{
    public class CustomTabbedPageRenderer : TabbedPageRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            
            //apply template resources
            this.Template = App.Current.Resources["PivotTemplate"] as ControlTemplate;
            this.HeaderTemplate = App.Current.Resources["PivotHeaderTemplate"] as System.Windows.DataTemplate;
        }
    }
}
