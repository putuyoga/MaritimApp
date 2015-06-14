using MaritimApp.Libs;
using MaritimApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MaritimApp.Converters
{
    public class ImageCuacaConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //Prefix each platform
            var imagePrefix = DependencyService.Get<IPrefixName>().GetPrefixName("WeatherIcons/day/");
            var imagePrefix2 = DependencyService.Get<IPrefixName>().GetPrefixName("WeatherIcons/night/");
            ImageSource source;
            Cuaca selected = (Cuaca)value;

            
            var time = selected.Waktu;
            if(6 < time.Hour && time.Hour < 18)
            {
                source = ImageSource.FromFile(imagePrefix + selected.KodeCuaca +".png");
            }
            else
            {
                source = ImageSource.FromFile(imagePrefix2 + selected.KodeCuaca + ".png");
            }
            return source;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
