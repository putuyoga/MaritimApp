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
        private static ImageCuacaConverter imgConverter = new ImageCuacaConverter();
        private static ImageSource QuestionImage;
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //Prefix each platform

            var imagePrefix = DependencyService.Get<IPrefixName>().GetPrefixName(String.Empty);

            Lokasi selected = (Lokasi)value;
            var terdekat = selected.GetCuacaTerdekat();
            ImageSource source;

            //load up image question
            QuestionImage = QuestionImage == null ? ImageSource.FromFile(imagePrefix + "Question.png") : QuestionImage;
            source = QuestionImage;

            if(terdekat != null)
            {
                source = imgConverter.Convert(terdekat, typeof(ImageSource), null, culture) as ImageSource;
            }
            return source;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
