using MaritimApp.Libs;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MaritimApp.Converters
{
    public class ImageAnginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //Prefix tiap platform
            var imagePrefix = DependencyService.Get<IPrefixName>().GetPrefixName("WindIcons/");
            double kecepatan = Double.Parse(value.ToString());
            ImageSource source;

            if (kecepatan < 61.6)
            {
                source = ImageSource.FromFile(imagePrefix + "1.png");
            }
            else if (kecepatan < 85.1)
            {
                source = ImageSource.FromFile(imagePrefix + "2.png");
            }
            else
            {
                source = ImageSource.FromFile(imagePrefix + "3.png");
            }
            return source;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
