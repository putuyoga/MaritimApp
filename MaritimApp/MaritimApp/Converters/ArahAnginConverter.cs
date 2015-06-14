using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Reflection;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace MaritimApp.Converters
{
    public class ArahAnginConverter : IValueConverter
    {

        public static Dictionary<string, string> DictArahAngin;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            LoadResource();
            string code = (string)value;
            string keterangan;
            DictArahAngin.TryGetValue(code, out keterangan);
            return keterangan;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            LoadResource();
            return value;
        }

        public void LoadResource()
        {
            var resourcePrefix = "MaritimApp.Data.";

            if(DictArahAngin != null) return;

             DictArahAngin = new Dictionary<string, string>();

            // note that the prefix includes the trailing period '.' that is required
            var assembly = typeof(App).GetTypeInfo().Assembly;

            Stream stream = assembly.GetManifestResourceStream(resourcePrefix + "ArahAngin.txt");
            string text = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                text = reader.ReadToEnd();
                string[] dict_array = Regex.Split(text,"\r\n");
                foreach (string item in dict_array)
                {
                    string[] data = item.Split('\t');
                    DictArahAngin.Add(data[0], data[1]);
                }
            }
        }
        
    }
}
