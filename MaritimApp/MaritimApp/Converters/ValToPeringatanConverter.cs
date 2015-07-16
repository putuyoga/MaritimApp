using MaritimApp.Models;
using MaritimApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MaritimApp.Converters
{
    public class ValToPeringatanConverter : IValueConverter
    {
        public static KondisiCuacaConverter KondCuacaConverter = new KondisiCuacaConverter();

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            PengaturanViewModel pengaturanVM = new PengaturanViewModel();
            var selectedVal = (Cuaca)value;
            string selectedParam = (string)parameter;
            Color selectedColor = Color.Black;

            if((string)parameter == "JarakPandang")
            {
                float settingJarakPandangMin = pengaturanVM.GetValues<float>("JarakPandangMin");
                float jarakPandangMin = settingJarakPandangMin == 0 ? 6 : settingJarakPandangMin; //default

                //lakukan pengecekan dan ganti warna
                if(selectedVal.JarakPandang < jarakPandangMin)
                {
                    selectedColor = Color.Maroon;
                }

                //prepare text output
                string textOutput = String.Format("{0} km", selectedVal.JarakPandang);
                var formattedString = new FormattedString();
                formattedString.Spans.Add(new Span() { Text = textOutput, ForegroundColor = selectedColor });
                return formattedString;
            }
            else if((string)parameter == "KondisiCuaca")
            {
                int settingKondisiCuaca = pengaturanVM.GetValues<int>("KondisiCuacaMin");
                int kondisiCuacaMin = settingKondisiCuaca;

                //lakukan pengecekan dan ganti warna
                if ((kondisiCuacaMin == 0 && selectedVal.KodeCuaca > 176) || (kondisiCuacaMin == 1 && selectedVal.KodeCuaca != 113))
                    selectedColor = Color.Maroon;

                //prepare text output
                string textOutput = KondCuacaConverter.Convert(selectedVal.KodeCuaca, typeof(string), null, culture).ToString();
                var formattedString = new FormattedString();
                formattedString.Spans.Add(new Span() { Text = textOutput, ForegroundColor = selectedColor });
                return formattedString;
            }
            else if ((string)parameter == "TinggiGelombang")
            {
                float settingTinggiGelombang = pengaturanVM.GetValues<float>("TinggiGelombangMin");
                float tinggiGelombangMin = settingTinggiGelombang == 0 ? 2 : settingTinggiGelombang; //default

                //lakukan pengecekan, dan ganti warna
                if (selectedVal.TinggiGelombang > tinggiGelombangMin) selectedColor = Color.Maroon;

                //prepare output text
                string textOutput = String.Format("{0:F1} m", selectedVal.TinggiGelombang);
                var formattedString = new FormattedString();
                formattedString.Spans.Add(new Span() { Text = textOutput, ForegroundColor = selectedColor });
                return formattedString;
            }
            else if ((string)parameter == "TinggiGelombangMaks")
            {
                float settingTinggiGelombang = pengaturanVM.GetValues<float>("TinggiGelombangMin");
                float tinggiGelombangMin = settingTinggiGelombang == 0 ? 2 : settingTinggiGelombang; //default

                //lakukan pengecekan, dan ganti warna
                if (selectedVal.TinggiGelombangMaks > tinggiGelombangMin) selectedColor = Color.Maroon;

                //prepare output text
                string textOutput = String.Format("{0:F1} m", selectedVal.TinggiGelombangMaks);
                var formattedString = new FormattedString();
                formattedString.Spans.Add(new Span() { Text = textOutput, ForegroundColor = selectedColor });
                return formattedString;
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return 1;
        }
    }
}
