using MaritimApp.Libs;
using MaritimApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MaritimApp.Converters
{
    public class KondisiCuacaFeaturedConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //Prefix each platform
            #if __ANDROID__
            var imagePrefix = "";
            #endif
            #if WINDOWS_PHONE
            var imagePrefix = "Assets/";
            #endif

            var imagePrefix = DependencyService.Get<IPrefixName>().GetPrefixName(String.Empty);

            Lokasi selected = (Lokasi)value;
            var terdekat = selected.GetCuacaTerdekat();
            ImageSource source;
            if(terdekat != null)
            {
                var imgConverter = new ImageCuacaConverter();
                source = (ImageSource)imgConverter.Convert(terdekat, null, null, null);
            }
            else
            {
                source = ImageSource.FromFile(imagePrefix + "Question.png");
            }
            return source;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
