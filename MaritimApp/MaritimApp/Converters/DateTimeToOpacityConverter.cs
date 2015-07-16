using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MaritimApp.Converters
{
    public class DateTimeToOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var selected = (DateTime)value;

            if (DateTime.Now.TimeOfDay.TotalHours > selected.TimeOfDay.TotalHours + 3)
            {
                return 0.7d;
            }
            else
            {
                return 1d;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return DateTime.Now;
        }
    }
}
