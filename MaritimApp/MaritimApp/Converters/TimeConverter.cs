using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MaritimApp.Converters
{
    public class TimeConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var waktu = (DateTime)value;
            return waktu.ToString("H:mm");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
