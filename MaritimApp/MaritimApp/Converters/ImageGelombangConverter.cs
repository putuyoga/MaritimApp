using MaritimApp.Libs;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MaritimApp.Converters
{
    public class ImageGelombangConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ImageSource source;
            var imagePrefix = DependencyService.Get<IPrefixName>().GetPrefixName("GelombangIcons/");
            double tinggi = Double.Parse(value.ToString());
            
            if (tinggi < 2)
            {
                source = ImageSource.FromFile(imagePrefix + "1.png");
            } 
            else if(tinggi < 4)
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
