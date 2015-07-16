using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;
using System.Reflection;
using System.Text.RegularExpressions;

namespace MaritimApp.Converters
{
    public class KondisiCuacaConverter : IValueConverter
    {
        public static Dictionary<int, string> DictKondisiCuaca;

        public KondisiCuacaConverter()
        {
            LoadResource();
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int code = (int)value;
            string keterangan;
            DictKondisiCuaca.TryGetValue(code, out keterangan);
            return keterangan;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //LoadResource();
            return value;
        }

        public void LoadResource()
        {
            var resourcePrefix = "MaritimApp.Data.";
            if (DictKondisiCuaca != null) return;

            DictKondisiCuaca = new Dictionary<int, string>();

            // note that the prefix includes the trailing period '.' that is required
            var assembly = typeof(App).GetTypeInfo().Assembly;

            Stream stream = assembly.GetManifestResourceStream(resourcePrefix + "KondisiCuaca.txt");
            string text = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                text = reader.ReadToEnd();
                string[] dict_array = Regex.Split(text, "\r\n");
                foreach (string item in dict_array)
                {
                    string[] data = item.Split('\t');
                    DictKondisiCuaca.Add(Int32.Parse(data[0]), data[1]);
                }
            }
        }
    }
}
